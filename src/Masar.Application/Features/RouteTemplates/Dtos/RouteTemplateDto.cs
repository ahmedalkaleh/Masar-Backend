using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.RouteTemplates.Dtos;

namespace Masar.Application.Features.RouteTemplates.Dtos
{
    public class RouteTemplateDto
    {
        public Guid RouteTemplateId { get; set; }
        public string TemplateName { get; set; } = null!;

        public Guid StartStationId { get; set; }
        public Guid EndStationId { get; set; }
        
        public List<RouteTemplateStopDto> RouteTemplateStops { get; set; } = [];
    }
}
