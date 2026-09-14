using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trips;
using Masar.Domain.TripStops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
namespace Masar.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommandHandler(ILogger<CreateTripCommandHandler> logger
        ,IAppDbContext context) : IRequestHandler<CreateTripCommand, Result<TripDto>>
    {
        private readonly ILogger<CreateTripCommandHandler> _logger;
        private readonly IAppDbContext _context;

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
                .FirstOrDefaultAsync(rt => rt.Id == command.RoutTemplateId , ct);

            if (routeTemplate == null)
            {
                _logger.LogWarning("Trip creation aborted: Route template with ID '{RouteTemplateId}' not found.", command.RoutTemplateId);
                return TripErrors.RouteTemplateNotFound;
            }
            DateTime currentDeparture = command.DepartureTime;
            var calculatedStops = new List<TripStop>();
            var sortedTemplateStops = routeTemplate.RouteTemplateStops.OrderBy(s => s.StopOrder).ToList();
            var allStationIds = sortedTemplateStops.Select(s => s.StationId).ToList();

            var routeSegments = await _context.RouteSegments
                .Where(s => allStationIds.Contains(s.FirstStationId) && allStationIds.Contains(s.SecondStationId))
                .ToListAsync(ct);

            for (int i = 1; i < sortedTemplateStops.Count; i++)
            {
                var prevTemplateStop = sortedTemplateStops[i - 1];
                var currentTemplateStop = sortedTemplateStops[i];

                var segment = routeSegments.FirstOrDefault(s =>
                    (s.FirstStationId == prevTemplateStop.StationId && s.SecondStationId == currentTemplateStop.StationId) ||
                    (s.FirstStationId == currentTemplateStop.StationId && s.SecondStationId == prevTemplateStop.StationId));

                if (segment == null) {
                    _logger.LogWarning("Trip creation aborted: Route segment between stations '{PrevStationId}' and '{CurrentStationId}' not found.", prevTemplateStop.StationId, currentTemplateStop.StationId);
                    return TripErrors.SegmentNotFound;
                }

                double pureTimeMinutes = ((double)segment.DistanceKm / (double)existingTrain.MaxSpeedKmh) * 60;
                double paddedTimeMinutes = pureTimeMinutes * 1.05;

                DateTime arrivalTime = currentDeparture.AddMinutes(paddedTimeMinutes);
                DateTime departureTime = arrivalTime.AddMinutes(5);

                bool isLastStation = (i == sortedTemplateStops.Count - 1);

                if (!isLastStation)
                {
                    var tripStopResult = TripStop.Create(
                        id: Guid.NewGuid(),
                        stationId: currentTemplateStop.StationId,
                        stopOrder: currentTemplateStop.StopOrder,
                        scheduledArrival: arrivalTime,
                        dwellTimeMinutes:5
                    );

                    if (tripStopResult.IsError)
                        return tripStopResult.Errors;

                    calculatedStops.Add(tripStopResult.Value);
                }

                currentDeparture = departureTime;

            }
        }

    }
}
