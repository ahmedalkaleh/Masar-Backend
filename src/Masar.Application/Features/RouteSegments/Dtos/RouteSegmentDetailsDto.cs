using Masar.Domain.RouteSegments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Dtos
{
    public class RouteSegmentDetailsDto
    {
        public Guid RouteSegmentID { get; set; }

        public Guid FirstStationId { get; set; }
        public string FirstStationNameAr { get; set; } = string.Empty;
        public string FirstStationNameEn { get; set; } = string.Empty;

        public Guid SecondStationId { get; set; }
        public string SecondStationNameAr { get; set; } = string.Empty;
        public string SecondStationNameEn { get; set; } = string.Empty;

        public TrackType TrackType { get; set; }

        public decimal DistanceKm { get; set; }

        public int EstPassengerTimeMin { get; set; }

        public string CorridorName { get; set; } = String.Empty;
    }
}
