using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate
{
    public record CreateRouteTemplateCommand(string TemplateName, Guid StartStationId, Guid EndStationId, List<CreateRouteTemplateStopCommand> RouteTemplateStops) : IRequest<Result<RouteTemplateDto>>;
   
}
