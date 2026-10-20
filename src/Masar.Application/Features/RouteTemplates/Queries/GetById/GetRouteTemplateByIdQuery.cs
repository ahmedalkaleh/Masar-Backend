using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Application.Features.RouteSegments.Queries.GetById;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Queries.GetById
{
    public sealed record GetRouteTemplateByIdQuery(Guid Id) : IRequest<Result<RouteTemplateDetailsDto>>;
}
