namespace Masar.Application.Features.RouteTemplates.Dtos
{
    public class RouteTemplateSummaryDto
    {
        public Guid RouteTemplateId { get; set; }
        public string TemplateName { get; set; } = string.Empty;

        public Guid StartStationId { get; set; }
        public string StartStationNameAr { get; set; } = string.Empty;
        public string StartStationNameEn { get; set; } = string.Empty;

        public Guid EndStationId { get; set; }
        public string EndStationNameAr { get; set; } = string.Empty;
        public string EndStationNameEn { get; set; } = string.Empty;
    }
}
