using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using Masar.Domain.RouteTemplates;
using Masar.Domain.RouteTemplateStops;
using Masar.Domain.TripStops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RoutTemplates.Commands.CreateRoutTemplate
{
    public class CreateRouteTemplateCommandHandler(
        ILogger<CreateRouteTemplateCommandHandler> logger,
        IAppDbContext context
        ) : IRequestHandler<CreateRouteTemplateCommand, Result<RouteTemplateDto>>
    {
        private readonly ILogger<CreateRouteTemplateCommandHandler> _logger = logger;
        private readonly IAppDbContext _context = context;

        public async Task<Result<RouteTemplateDto>> Handle(CreateRouteTemplateCommand command, CancellationToken ct)
        {
            var taplateName = command.TemplateName.Trim().ToLower();

            if (!await _context.RouteTemplates.AnyAsync(rt => rt.TemplateName.ToLower() == taplateName, ct))
            {
                  _logger.LogWarning("RouteTemplate creation aborted: Template with name '{TemplateName}' already exists.", command.TemplateName);
                return RouteTemplateErrors.RouteTemplateExists; 
            }

            if (!await _context.Stations.AnyAsync(x => x.Id == command.StartStationId, ct))
            {
                _logger.LogWarning("RouteTemplate creation aborted: Start station with ID '{StartStationId}' not found.", command.StartStationId);
                return RouteTemplateErrors.StartStationNotFound;
            }

            if (!await _context.Stations.AnyAsync(x => x.Id == command.EndStationId, ct))
            {
                _logger.LogWarning("RouteTemplate creation aborted: End station with ID '{EndStationId}' not found.", command.EndStationId);
                return RouteTemplateErrors.EndStationNotFound;
            }

            if (command.StartStationId == command.EndStationId)
            {
                _logger.LogWarning("RouteTemplate creation aborted: Start station ID '{StartStationId}' is the same as end station ID '{EndStationId}'.", command.StartStationId, command.EndStationId);
                return RouteTemplateErrors.EndStationEqualStartStation;
            }



            List<RouteTemplateStop> routeTemplateStops = [];

            if (command.RouteTemplateStops != null)
            {

                List<RouteSegment> routSegments = new List<RouteSegment>();

                if (command.RouteTemplateStops.Count > 0)
                {
                    var allStationIDs = command.RouteTemplateStops.Select(x => x.StationId).ToList();
                    allStationIDs.Add(command.StartStationId);
                    allStationIDs.Add(command.EndStationId);
                    routSegments = await _context.RouteSegments.Where(x => allStationIDs.Contains(x.FirstStationId) && allStationIDs.Contains(x.SecondStationId)).ToListAsync(ct);
                }

                var RouteTemplateStopsSorted = command.RouteTemplateStops.OrderBy(x => x.StopOrder).ToList(); ;


                if (RouteTemplateStopsSorted.Count > 0 && RouteTemplateStopsSorted.Count != RouteTemplateStopsSorted.Last().StopOrder)
                {
                    _logger.LogWarning("RouteTemplate creation aborted: Inconsistent stop orders found.");
                    return RouteTemplateStopErrors.InconsistentStopOrders;
                }

                var fullPathIds = new List<Guid> { command.StartStationId };
                fullPathIds.AddRange(RouteTemplateStopsSorted.Select(x => x.StationId));
                fullPathIds.Add(command.EndStationId);

                for (int i = 0; i < fullPathIds.Count - 1; i++)
                {

                    if (!routSegments.Any(rs => rs.FirstStationId == RouteTemplateStopsSorted[i].StationId && rs.SecondStationId == RouteTemplateStopsSorted[i + 1].StationId ||
                                             rs.FirstStationId == RouteTemplateStopsSorted[i + 1].StationId && rs.SecondStationId == RouteTemplateStopsSorted[i].StationId))
                    {
                        _logger.LogWarning("RouteTemplate creation aborted: Route segment from station ID '{FirstStationId}' to station ID '{SecondStationId}' does not exist.", RouteTemplateStopsSorted[i].StationId, RouteTemplateStopsSorted[i + 1].StationId);
                        return RouteTemplateStopErrors.RouteSegmentNotFound;
                    }

                }

                foreach (var stop in command.RouteTemplateStops)
                {
                    var routeTemplateStopResult = RouteTemplateStop.Create(Guid.NewGuid(), stop.StationId, stop.StopOrder);
                    if (routeTemplateStopResult.IsError)
                    {
                        _logger.LogWarning("RouteTemplate creation aborted: Failed to create RouteTemplateStop for station ID '{StopStationId}'.", stop.StationId);
                        return routeTemplateStopResult.Errors;
                    }
                    if (routeTemplateStops.Any(rts => rts.StationId == stop.StationId))
                    {
                        _logger.LogWarning("RouteTemplate creation aborted: Duplicate station ID '{StopStationId}' found.", stop.StationId);
                        return RouteTemplateStopErrors.DuplicateStop;
                    }
                    routeTemplateStops.Add(routeTemplateStopResult.Value);

                }

            }



            var CreateRouteTemplateResult = RouteTemplate.Create(Guid.NewGuid(), command.TemplateName.Trim(), command.StartStationId, command.EndStationId, routeTemplateStops);
            if (CreateRouteTemplateResult.IsError)
            {
                return CreateRouteTemplateResult.Errors;
            }
            _context.RouteTemplates.Add(CreateRouteTemplateResult.Value);

            await _context.SaveChangesAsync(ct);
            var routeTemplate = CreateRouteTemplateResult.Value;
            _logger.LogInformation("RouteTemplate with ID '{RouteTemplateId}' created successfully.", routeTemplate.Id);
            return CreateRouteTemplateResult.Errors;
        } 
    }
}
