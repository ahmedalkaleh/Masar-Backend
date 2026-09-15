using Masar.Application.Common.Interfaces;
using Masar.Domain.Trips;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Infrastructure.Services
{
    public class TripCollisionChecker(IAppDbContext context) : ITripCollisionChecker
    {
        
            public async Task<bool> HasCollisionAsync(
        List<SegmentOccupancy> newTripOccupancies,
        DateTime overallStartTime,
        DateTime overallEndTime,
        CancellationToken ct)
        {
            var overlappingTrips = await context.Trips
                .Include(t => t.TripStops)
                .Where(t=>
                            t.DepartureTime < overallEndTime &&
                            t.EstimatedArrivalTime > overallStartTime)
                .ToListAsync(ct);

            foreach (var existingTrip in overlappingTrips)
            {
                var existingOccupancies = ExtractSegmentOccupancies(existingTrip);

                foreach (var newOcc in newTripOccupancies)
                {
                    foreach (var existOcc in existingOccupancies)
                    {
                        bool isSameSegment =
                            (newOcc.FirstStationId == existOcc.FirstStationId && newOcc.SecondStationId == existOcc.SecondStationId) ||
                            (newOcc.FirstStationId == existOcc.SecondStationId && newOcc.SecondStationId == existOcc.FirstStationId);

                        if (isSameSegment && newOcc.EntryTime < existOcc.ExitTime && existOcc.EntryTime < newOcc.ExitTime)
                        {
                            return true; 
                        }
                    }
                }
            }

            return false;
        }
        private List<SegmentOccupancy> ExtractSegmentOccupancies(Trip trip)
        {
            var occupancies = new List<SegmentOccupancy>();

            var sortedStops = trip.TripStops.OrderBy(s => s.StopOrder).ToList();

            DateTime currentDeparture = trip.DepartureTime;

            for (int i = 0; i < sortedStops.Count; i++)
            {
                Guid prevStationId = (i == 0) ? trip.OriginStationId : sortedStops[i - 1].StationId;
                var currentStop = sortedStops[i];

                occupancies.Add(new SegmentOccupancy(
                    prevStationId,
                    currentStop.StationId,
                    currentDeparture,
                    currentStop.ScheduledArrival
                ));

                currentDeparture = currentStop.ScheduledArrival.AddMinutes(currentStop.DwellTimeMinutes);
            }

            Guid lastStationId = sortedStops.Count > 0
                ? sortedStops.Last().StationId
                : trip.OriginStationId;

            occupancies.Add(new SegmentOccupancy(
                lastStationId,
                trip.DestinationStationId,
                currentDeparture,
                trip.EstimatedArrivalTime
            ));

            return occupancies;
        }
    }
    
}
