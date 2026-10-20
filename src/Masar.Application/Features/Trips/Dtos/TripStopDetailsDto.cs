namespace Masar.Application.Features.Trips.Dtos
{
    public class TripStopDetailsDto
    {
        public Guid TripStopId { get; set; }
        public Guid StationId { get; set; }
        public string StationNameAr { get; set; } = string.Empty;
        public string StationNameEn { get; set; } = string.Empty;

        public int StopOrder { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public int DwellTimeMinutes { get; set; }
    }
}
