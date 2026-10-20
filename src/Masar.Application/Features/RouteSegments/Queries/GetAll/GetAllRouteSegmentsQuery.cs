using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Queries.GetAll
{
    public sealed record GetAllRouteSegmentsQuery : IRequest<Result<List<RouteSegmentSummaryDto>>>;
}
