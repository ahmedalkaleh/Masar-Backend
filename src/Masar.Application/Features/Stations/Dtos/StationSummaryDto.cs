namespace Masar.Application.Features.Stations.Dtos
{
    public class StationSummaryDto
    {
        public Guid StationID { get; set; }

        public string NameAr { get; set; } = String.Empty;

        public string NameEn { get; set; } = String.Empty;

        public string Governorate { get; set; } = String.Empty;

    }
}
