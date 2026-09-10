using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate
{
    public sealed record CreateRouteTemplateStopCommand(Guid StationId, int StopOrder) : IRequest<Result<RouteTemplateStopDto>>;
   
}
