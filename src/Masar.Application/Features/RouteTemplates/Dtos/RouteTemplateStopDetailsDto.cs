using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Dtos
{
    public class RouteTemplateStopDetailsDto
    {
        public Guid RouteTemplateStopId;

        public Guid StationId;
        public string StationNameAr { get; set; } = string.Empty;
        public string StationNameEn { get; set; } = string.Empty;

        public int StopOrder;
    }
}
