using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Queries.GetAll
{
    public class GetAllRouteSegmentsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllRouteSegmentsQuery, Result<List<RouteSegmentSummaryDto>>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<List<RouteSegmentSummaryDto>>> Handle(GetAllRouteSegmentsQuery query, CancellationToken cancellationToken)
        {
            var routeSegments = await _context.RouteSegments.AsNoTracking()
                  .Select(x => 
                  new RouteSegmentSummaryDto
                  {
                      RouteSegmentID = x.Id,
                      FirstStationId = x.FirstStationId,
                      FirstStationNameAr = x.FirstStation.NameAr,
                      FirstStationNameEn = x.FirstStation.NameEn,
                      SecondStationId = x.SecondStationId,
                      SecondStationNameAr = x.SecondStation.NameAr,
                      SecondStationNameEn = x.SecondStation.NameEn,
                  })
                  .OrderBy(x => x.FirstStationNameAr)
                  .ThenBy(x => x.FirstStationNameEn)
                  .ToListAsync(cancellationToken);

            return routeSegments;
        }
    }
}
