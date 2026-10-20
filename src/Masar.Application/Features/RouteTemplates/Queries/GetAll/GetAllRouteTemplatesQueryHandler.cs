using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.RouteTemplates.Queries.GetAll
{
    public class GetAllRouteTemplatesQueryHandler(IAppDbContext context) : IRequestHandler<GetAllRouteTemplatesQuery, Result<List<RouteTemplateSummaryDto>>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<List<RouteTemplateSummaryDto>>> Handle(GetAllRouteTemplatesQuery query, CancellationToken cancellationToken)
        {
            var routeTemplates = await _context.RouteTemplates.AsNoTracking()
                .Select(x => new RouteTemplateSummaryDto
                {
                    RouteTemplateId = x.Id,
                    TemplateName = x.TemplateName,
                    StartStationId = x.StartStationId,
                    StartStationNameAr = x.StartStation.NameAr,
                    StartStationNameEn = x.StartStation.NameEn,
                    EndStationId = x.EndStationId,
                    EndStationNameAr = x.EndStation.NameAr,
                    EndStationNameEn = x.EndStation.NameEn,
                })
                .OrderBy(x => x.TemplateName)
                .ThenBy(x => x.StartStationNameEn)
                .ToListAsync(cancellationToken);


            return routeTemplates;

        }
    }
}
