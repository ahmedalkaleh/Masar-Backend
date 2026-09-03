using Masar.Domain.RouteSegments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Dtos
{
    public class RouteSegmentDto
    {
        public Guid RouteSegmentID { get; set; }

        public Guid FromStationId { get; set; }

        public Guid ToStationId { get; set; }

        public TrackType TrackType { get; set; }

        public decimal DistanceKm { get; set; }

        public int EstPassengerTimeMin { get; set; }

        public string CorridorName { get; set; } = String.Empty;
    }
}
