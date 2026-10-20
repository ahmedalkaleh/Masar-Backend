using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Trains;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Mappers
{
    public static class TripMapper
    {
        public static TripDto ToDto(this Trip trip)
        {

            return new TripDto
            {
                TripId = trip.Id,
                DepartureTime = trip.DepartureTime,
                DestinationStationId = trip.DestinationStationId,
                EstimatedArrivalTime = trip.EstimatedArrivalTime,
                OriginStationId = trip.OriginStationId,
                TripStops = trip.TripStops?.Select(ts => new TripStopDto
                (
                   ts.Id,
                   ts.StationId,
                   ts.StopOrder,
                   ts.ScheduledArrival,
                   ts.ScheduledDeparture,
                   ts.DwellTimeMinutes
                )).ToList()
            };
        }

        public static List<TripDto> ToDtos(this IEnumerable<Trip> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
