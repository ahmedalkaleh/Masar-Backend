using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Queries.GetById
{
    public class GetRouteSegmentByIdQueryHandler(IAppDbContext context, ILogger<GetRouteSegmentByIdQuery> logger) : IRequestHandler<GetRouteSegmentByIdQuery, Result<RouteSegmentDetailsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetRouteSegmentByIdQuery> _logger = logger;

        public async Task<Result<RouteSegmentDetailsDto>> Handle(GetRouteSegmentByIdQuery query, CancellationToken cancellationToken)
        {


            var routeSegment = await _context.RouteSegments.AsNoTracking()
                .Select(x => 
                new RouteSegmentDetailsDto
                {
                    RouteSegmentID = x.Id,
                    FirstStationId = x.FirstStationId,
                    FirstStationNameAr = x.FirstStation.NameAr,
                    FirstStationNameEn = x.FirstStation.NameEn,
                    SecondStationId = x.SecondStationId,
                    SecondStationNameAr = x.SecondStation.NameAr,
                    SecondStationNameEn = x.SecondStation.NameEn,
                    EstPassengerTimeMin = x.EstPassengerTimeMin,
                    TrackType = x.TrackType,
                    DistanceKm = x.DistanceKm,
                    CorridorName = x.CorridorName
                })
                .FirstOrDefaultAsync(x => x.RouteSegmentID == query.Id, cancellationToken);

            if (routeSegment == null)
            {
                _logger.LogWarning("RouteSegment get aborted. RouteSegment with id {RouteSegmentId} not found.", query.Id);
                return RouteSegmentErrors.RouteSegmentNotFound;
            }

            return routeSegment;
        }

    }
}
