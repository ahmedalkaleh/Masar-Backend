using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using Masar.Domain.RouteTemplateStops;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RoutTemplates.Commands.CreateRoutTemplate
{
    public class CreateRouteTemplateCommandHandler(
        ILogger<CreateRouteTemplateCommandHandler> logger,
        IAppDbContext Context
        ) : IRequestHandler<CreateRouteTemplateCommand, Result<RouteTemplateDto>>
    {
        private readonly ILogger<CreateRouteTemplateCommandHandler> _logger;
        private readonly IAppDbContext _context;

        public async Task<Result<RouteTemplateDto>> Handle(CreateRouteTemplateCommand command, CancellationToken ct)
        {
            var taplateName = command.TemplateName.Trim().ToLower();
            var existingTemplate = _context.RouteTemplates.FirstOrDefault(rt => rt.TemplateName.ToLower() == taplateName);
            if (existingTemplate != null)
            {
                  _logger.LogWarning("RouteTemplate creation aborted: Template with name '{TemplateName}' already exists.", command.TemplateName);
                return RouteTemplateErrors.RouteTemplateExists; 
            }

            var existingStartStation = await _context.Stations.FindAsync(new object[] { command.StartStationId }, ct);
            if (existingStartStation == null|| existingStartStation.IsDelete)
            {
                _logger.LogWarning("RouteTemplate creation aborted: Start station with ID '{StartStationId}' not found.", command.StartStationId);
                return RouteTemplateErrors.StartStationNotFound;
            }
            var existingEndStation = await _context.Stations.FindAsync(new object[] { command.EndStationId }, ct);
            if (existingEndStation == null || existingEndStation.IsDelete)
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

            foreach (var stop in command.RouteTemplateStops)
            {
                var existingStopStation = await _context.Stations.FindAsync(new object[] { stop.StationId }, ct);
                if (existingStopStation == null || existingStopStation.IsDelete)
                {
                    _logger.LogWarning("RouteTemplate creation aborted: Stop station with ID '{StopStationId}' not found.", stop.StationId);
                    return RouteTemplateStopErrors.StationNotFound;
                }
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

            routeTemplateStops.Sort((a, b) => a.StopOrder.CompareTo(b.StopOrder));
            if (routeTemplateStops.Count > 0 && !_context.RouteSegments.Any(rs => rs.FromStationId == command.StartStationId && rs.ToStationId == routeTemplateStops[0].StationId))
            {
                _logger.LogWarning("RouteTemplate creation aborted: Route segment from  station ID '{StartStationId}' to  station ID '{EndStationId}' does not exist.", command.StartStationId, routeTemplateStops[0].StationId);
                return RouteTemplateStopErrors.RouteSegmentNotFound;
            }
            for(int i = 0; i < routeTemplateStops.Count - 1; i++)
            {
                if (!_context.RouteSegments.Any(rs => rs.FromStationId == routeTemplateStops[i].StationId && rs.ToStationId == routeTemplateStops[i + 1].StationId))
                {
                    _logger.LogWarning("RouteTemplate creation aborted: Route segment from station ID '{FromStationId}' to station ID '{ToStationId}' does not exist.", routeTemplateStops[i].StationId, routeTemplateStops[i + 1].StationId);
                    return RouteTemplateStopErrors.RouteSegmentNotFound;
                }
            }
            if(routeTemplateStops.Count > 0 && !_context.RouteSegments.Any(rs => rs.FromStationId == routeTemplateStops[routeTemplateStops.Count - 1].StationId && rs.ToStationId == command.EndStationId))
            {
                _logger.LogWarning("RouteTemplate creation aborted: Route segment from station ID '{FromStationId}' to station ID '{ToStationId}' does not exist.", routeTemplateStops[routeTemplateStops.Count - 1].StationId, command.EndStationId);
                return RouteTemplateStopErrors.RouteSegmentNotFound;
            }

            var CreateRouteTemplateResult = RouteTemplate.Create(Guid.NewGuid(), command.TemplateName, command.StartStationId, command.EndStationId, routeTemplateStops);
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
