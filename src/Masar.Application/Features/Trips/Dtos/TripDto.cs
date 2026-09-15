using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Dtos
{
    public class TripDto()
    {
        public Guid TripId { get; set; }
        public Guid originStationId { get; set; }
        public Guid DestinationStationId { get; set; }

        public DateTime DepartureTime { get; set; }
        public DateTime EstimatedArrivalTime { get; set; }

        public DateTime ActualArrivalTime { get; set; }

        public string status { get; set; } = String.Empty;
        public List<TripStopDto> TripStops { get; set; } = new List<TripStopDto>();
    }
}
