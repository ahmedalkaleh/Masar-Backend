using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Commands.UpdateRouteSegment
{
    public sealed record UpdateRouteSegmentCommand(
    Guid RouteSegmentID,
    Guid FromStationId,
    Guid ToStationId,
    TrackType TrackType,
    decimal DistanceKm,
    int EstPassengerTimeMin,
    string CorridorName) : IRequest<Result<Updated>>
    {
    }
}
