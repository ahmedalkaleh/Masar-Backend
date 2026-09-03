using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.RouteSegments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Mappers
{
    public static class RouteSegmentMapper
    {
        public static RouteSegmentDto ToDto(this RouteSegment routeSegment)
        {
            return new RouteSegmentDto
            {
                RouteSegmentID = routeSegment.Id,
                FromStationId = routeSegment.FromStationId,
                ToStationId = routeSegment.ToStationId,
                TrackType = routeSegment.TrackType,
                DistanceKm = routeSegment.DistanceKm,
                EstPassengerTimeMin = routeSegment.EstPassengerTimeMin,
                CorridorName = routeSegment.CorridorName
            };
        }

        public static List<RouteSegmentDto> ToDto(this IEnumerable<RouteSegment> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }
    }
}
