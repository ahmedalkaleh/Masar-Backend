using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Commands.CreateRouteSegment
{
    public sealed record CreateRouteSegmentCommand : IRequest<Result<RouteSegmentDto>>
    {
        /// <summary>
        /// The first station of the route segment.
        /// </summary>
        public Guid FirstStationId { get; init; }

        /// <summary>
        /// The second station of the route segment.
        /// </summary>
        public Guid SecondStationId { get; init; }

        /// <summary>
        /// The type of track used by the route segment.
        /// 0 = Single, 1 = Double.
        /// </summary>
        public TrackType TrackType { get; init; }

        /// <summary>
        /// The distance of the route segment in kilometers.
        /// Must be between 1 and 9999.99.
        /// </summary>
        public decimal DistanceKm { get; init; }

        /// <summary>
        /// The estimated passenger travel time in minutes.
        /// Must be greater than 0.
        /// </summary>
        public int EstPassengerTimeMin { get; init; }

        /// <summary>
        /// The name of the corridor containing the route segment.
        /// Maximum length: 100 characters.
        /// </summary>
        public string CorridorName { get; init; } = null!;
    }
}
