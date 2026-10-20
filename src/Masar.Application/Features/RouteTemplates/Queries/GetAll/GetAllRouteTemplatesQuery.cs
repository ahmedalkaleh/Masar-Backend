using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Application.Features.RouteTemplates.Queries.GetById;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Queries.GetAll
{
    public sealed record GetAllRouteTemplatesQuery : IRequest<Result<List<RouteTemplateSummaryDto>>>;
}
