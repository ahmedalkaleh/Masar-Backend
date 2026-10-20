using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.RouteTemplates.Queries.GetById
{
    public class GetRouteTemplateByIdQueryHandler(IAppDbContext context, ILogger<GetRouteTemplateByIdQuery> logger) : IRequestHandler<GetRouteTemplateByIdQuery, Result<RouteTemplateDetailsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetRouteTemplateByIdQuery> _logger = logger;

        public async Task<Result<RouteTemplateDetailsDto>> Handle(GetRouteTemplateByIdQuery query, CancellationToken cancellationToken)
        {
            var routeTemplate = await _context.RouteTemplates.AsNoTracking()
                .Select(x => new RouteTemplateDetailsDto
                {
                    RouteTemplateId = x.Id,
                    TemplateName = x.TemplateName,
                    StartStationId = x.StartStationId,
                    StartStationNameAr = x.StartStation.NameAr,
                    StartStationNameEn = x.StartStation.NameEn,
                    EndStationId = x.EndStationId,
                    EndStationNameAr = x.EndStation.NameAr,
                    EndStationNameEn = x.EndStation.NameEn,
                    RouteTemplateStops = x.RouteTemplateStops.Select(x =>
                    new RouteTemplateStopDetailsDto
                    {
                        RouteTemplateStopId = x.Id,
                        StationId = x.StationId,
                        StationNameAr = x.Station.NameAr,
                        StationNameEn = x.Station.NameEn,
                        StopOrder = x.StopOrder
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.RouteTemplateId == query.Id, cancellationToken);

            if(routeTemplate == null)
            {
                _logger.LogWarning("RouteTemplate get aborted. RouteTemplate with id {RouteTemplateId} not found.", query.Id);
                return RouteTemplateErrors.RouteTemplateNotFound;
            }

            return routeTemplate;

        }


    }
}
