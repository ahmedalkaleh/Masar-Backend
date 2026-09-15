using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplateStops;
using Masar.Domain.Trips;
using Masar.Domain.TripStops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Masar.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommandHandler(
        ILogger<CreateTripCommandHandler> logger,
        IAppDbContext context, ITripCollisionChecker collisionChecker) : IRequestHandler<CreateTripCommand, Result<TripDto>>
    {
        ILogger<CreateTripCommandHandler> _logger = logger;
        IAppDbContext _context = context;
        ITripCollisionChecker _collisionChecker = collisionChecker;
        public async Task<Result<TripDto>> Handle(CreateTripCommand command, CancellationToken ct)
        {
            var existingTrain = await _context.Trains.FindAsync(new object[] { command.TrainId }, ct);
            if (existingTrain == null || existingTrain.IsDelete)
            {
                _logger.LogWarning("Trip creation aborted: Train with ID '{TrainId}' not found.", command.TrainId);
                return TripErrors.TrainNotFound;
            }

            var routeTemplate = await _context.RouteTemplates
                .Include(rt => rt.RouteTemplateStops.OrderBy(s => s.StopOrder))
                    .ThenInclude(s => s.Station)
                .FirstOrDefaultAsync(rt => rt.Id == command.RoutTemplateId, ct);

            if (routeTemplate == null)
            {
                _logger.LogWarning("Trip creation aborted: Route template with ID '{RouteTemplateId}' not found.", command.RoutTemplateId);
                return TripErrors.RouteTemplateNotFound;
            }

            DateTime currentDeparture = command.DepartureTime;
            var calculatedStops = new List<TripStop>();
            var sortedTemplateStops = routeTemplate.RouteTemplateStops.OrderBy(s => s.StopOrder).ToList();

            // تضمين محطات البداية والنهاية لضمان جلب السكك حتى لو كانت القائمة فارغة
            var allStationIds = sortedTemplateStops.Select(s => s.StationId).ToList();
            allStationIds.Add(routeTemplate.StartStationId);
            allStationIds.Add(routeTemplate.EndStationId);

            var routeSegments = await _context.RouteSegments
                .Where(s => allStationIds.Contains(s.FirstStationId) && allStationIds.Contains(s.SecondStationId))
                .ToListAsync(ct);

            DateTime lastArrivalTime;

            // --- 1. حساب التوقفات الوسطى إن وجدت ---
            for (int i = 0; i < sortedTemplateStops.Count; i++)
            {
                RouteTemplateStop prevTemplateStop;
                if (i == 0)
                    prevTemplateStop = RouteTemplateStop.Create(Guid.NewGuid(), routeTemplate.StartStationId, 0).Value;
                else
                    prevTemplateStop = sortedTemplateStops[i - 1];

                var currentTemplateStop = sortedTemplateStops[i];

                var segment = routeSegments.FirstOrDefault(s =>
                    (s.FirstStationId == prevTemplateStop.StationId && s.SecondStationId == currentTemplateStop.StationId) ||
                    (s.FirstStationId == currentTemplateStop.StationId && s.SecondStationId == prevTemplateStop.StationId));

                if (segment == null)
                {
                    _logger.LogWarning("Trip creation aborted: Route segment between stations '{PrevStationId}' and '{CurrentStationId}' not found.", prevTemplateStop.StationId, currentTemplateStop.StationId);
                    return TripErrors.SegmentNotFound;
                }

                double pureTimeMinutes = ((double)segment.DistanceKm / (double)existingTrain.MaxSpeedKmh) * 60;
                double paddedTimeMinutes = pureTimeMinutes * 1.05;

                DateTime arrivalTime = currentDeparture.AddMinutes(paddedTimeMinutes);
                DateTime departureTime = arrivalTime.AddMinutes(5);

                var tripStopResult = TripStop.Create(
                    id: Guid.NewGuid(),
                    stationId: currentTemplateStop.StationId,
                    stopOrder: currentTemplateStop.StopOrder,
                    scheduledArrival: arrivalTime,
                    dwellTimeMinutes: 5
                );

                if (tripStopResult.IsError)
                    return tripStopResult.Errors;

                calculatedStops.Add(tripStopResult.Value);

                currentDeparture = departureTime;
            }

            // --- 2. حساب القطاع الأخير للوصول إلى محطة النهاية ---
            Guid lastIntermediateStationId = sortedTemplateStops.Count > 0
                ? sortedTemplateStops.Last().StationId
                : routeTemplate.StartStationId;

            var finalSegment = routeSegments.FirstOrDefault(s =>
                (s.FirstStationId == lastIntermediateStationId && s.SecondStationId == routeTemplate.EndStationId) ||
                (s.FirstStationId == routeTemplate.EndStationId && s.SecondStationId == lastIntermediateStationId));

            if (finalSegment == null)
            {
                _logger.LogWarning("Trip creation aborted: Final route segment to station '{EndStationId}' not found.", routeTemplate.EndStationId);
                return TripErrors.SegmentNotFound;
            }

            double finalPureTimeMinutes = ((double)finalSegment.DistanceKm / (double)existingTrain.MaxSpeedKmh) * 60;
            double finalPaddedTimeMinutes = finalPureTimeMinutes * 1.05;

            lastArrivalTime = currentDeparture.AddMinutes(finalPaddedTimeMinutes);
            // 1. تجميع قطاعات الرحلة الجديدة
            var newTripOccupancies = new List<SegmentOccupancy>();
            DateTime stepDeparture = command.DepartureTime;

            for (int i = 0; i < sortedTemplateStops.Count; i++)
            {
                Guid prevStationId = (i == 0) ? routeTemplate.StartStationId : sortedTemplateStops[i - 1].StationId;
                var currentStop = calculatedStops[i];

                newTripOccupancies.Add(new SegmentOccupancy(
                    prevStationId,
                    currentStop.StationId,
                    stepDeparture,
                    currentStop.ScheduledArrival
                ));

                stepDeparture = currentStop.ScheduledArrival.AddMinutes(currentStop.DwellTimeMinutes);
            }

            Guid lastStationBeforeEnd = sortedTemplateStops.Count > 0
                ? sortedTemplateStops.Last().StationId
                : routeTemplate.StartStationId;

            newTripOccupancies.Add(new SegmentOccupancy(
                lastStationBeforeEnd,
                routeTemplate.EndStationId,
                stepDeparture,
                lastArrivalTime
            ));

            
            bool hasCollision = await _collisionChecker.HasCollisionAsync(
                newTripOccupancies,
                command.DepartureTime,
                lastArrivalTime,
                ct);

            if (hasCollision)
            {
                _logger.LogWarning("Trip creation aborted: Collision detected on single-track route.");
                return TripErrors.TemporalCollisionDetected; 
            }
            var CreateTripResult = Trip.Create(
                id: Guid.NewGuid(),
                trainId: command.TrainId,
                originStationId: routeTemplate.StartStationId,
                destinationStationId: routeTemplate.EndStationId,
                departureTime: command.DepartureTime,
                estimatedArrivalTime: lastArrivalTime,
                tripStops: calculatedStops
              
            );
            _context.Trips.Add(CreateTripResult.Value);
            await _context.SaveChangesAsync(ct);
            var trip= CreateTripResult.Value;
            _logger.LogInformation("Trip created successfully with ID '{TripId}' for Train ID '{TrainId}'.", trip.Id, trip.TrainId);
            return CreateTripResult.Errors;
        }

    }
}