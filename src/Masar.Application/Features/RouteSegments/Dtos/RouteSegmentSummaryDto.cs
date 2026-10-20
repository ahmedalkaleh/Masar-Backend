namespace Masar.Application.Features.RouteSegments.Dtos
{
    public class RouteSegmentSummaryDto
    {
        public Guid RouteSegmentID { get; set; }

        public Guid FirstStationId { get; set; }
        public string FirstStationNameAr { get; set; } = string.Empty;
        public string FirstStationNameEn { get; set; } = string.Empty;

        public Guid SecondStationId { get; set; }
        public string SecondStationNameAr { get; set; } = string.Empty;
        public string SecondStationNameEn { get; set; } = string.Empty;

    }
}
