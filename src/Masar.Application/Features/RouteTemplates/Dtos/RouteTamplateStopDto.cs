using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Dtos
{
    public sealed record RouteTemplateStopDto(Guid RouteTemplateStopId, Guid StationId, int StopOrder);
}
