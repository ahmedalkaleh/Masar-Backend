using Masar.Domain.Trains;

namespace Masar.Application.Features.Trains.Dtos
{
    public class TrainDetailsDto
    {
        public Guid TrainID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string TrainType { get; set; } = string.Empty;
        public int MaxSpeedKmh { get; set; }
        public TrainStatus Status { get; set; }
        public Guid? CurrentStationId { get; set; }
        public string? CurrentStationNameAr { get; set; }
        public string? CurrentStationNameEn { get; set; }

    }
}
