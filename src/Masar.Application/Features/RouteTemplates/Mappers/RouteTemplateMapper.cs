using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.RouteTemplates;
using Masar.Domain.RouteTemplateStops;
namespace Masar.Application.Features.RouteTemplates.Mappers
{
    public static class RouteTemplateMapper
    {
        public static RouteTemplateDto ToDto(this RouteTemplate entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            return new RouteTemplateDto { RouteTemplateId = entity.Id, TemplateName = entity.TemplateName, StartStationId = entity.StartStationId, EndStationId = entity.EndStationId, RouteTemplateStops = entity.RouteTemplateStops?.Select(s => s.ToDto()).ToList() ?? [] };
        }

        public static List<RouteTemplateDto> ToDtos(this IEnumerable<RouteTemplate> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }

        public static RouteTemplateStopDto ToDto(this RouteTemplateStop entity)
        {
            ArgumentNullException.ThrowIfNull(entity);
            return new RouteTemplateStopDto(entity.Id, entity.StationId, entity.StopOrder);
        }
        public static List<RouteTemplateStopDto> ToDtos(this IEnumerable<RouteTemplateStop> entities)
        {
            return [.. entities.Select(e => e.ToDto())];
        }
    }
}
