namespace Masar.Application.Features.Trips.Dtos
{
    public class TripSummaryDto
    {
        public Guid TripId { get; set; }
        public Guid OriginStationId { get; set; }
        public string OriginStationNameAr { get; set; } = string.Empty;
        public string OriginStationNameEn { get; set; } = string.Empty;

        public Guid DestinationStationId { get; set; }
        public string DestinationStationNameAr { get; set; } = string.Empty;
        public string DestinationStationNameEn { get; set; } = string.Empty;
    }

}
