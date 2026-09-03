using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Application.Features.RouteSegments.Mappers;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Commands.CreateRouteSegment
{
    public class CreateRouteSegmentCommandHandler(IAppDbContext context, ILogger<CreateRouteSegmentCommandHandler> logger) : IRequestHandler<CreateRouteSegmentCommand, Result<RouteSegmentDto>>
    {
        private readonly IAppDbContext _context = context;

        private readonly ILogger<CreateRouteSegmentCommandHandler> _logger = logger;

        public async Task<Result<RouteSegmentDto>> Handle(CreateRouteSegmentCommand command, CancellationToken cancellationToken)
        {
            if(!(await _context.Stations.AnyAsync(x => x.Id == command.FromStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment Creation aborted.FromStation with id {FromStationId} not found.", command.FromStationId);
                return RouteSegmentErrors.FromStationIdNotFound;
            }

            if (!(await _context.Stations.AnyAsync(x => x.Id == command.ToStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment Creation aborted.ToStation with id {ToStationId} not found.", command.ToStationId);
                return RouteSegmentErrors.ToStationIdNotFound;
            }

            if (await _context.RouteSegments.AnyAsync(x => x.FromStationId == command.FromStationId && x.ToStationId == command.ToStationId, cancellationToken))
            {
                _logger.LogWarning("RouteSegment Creation aborted.RouteSegment with FromStationId {FromStationId} and ToStationId {ToStationId} already exists.", command.FromStationId , command.ToStationId);
                return RouteSegmentErrors.RouteSegmentAlreadyExists;
            }


            if (await _context.RouteSegments.AnyAsync(x => x.CorridorName == command.CorridorName,cancellationToken))
            {
                _logger.LogWarning("RouteSegment Creation aborted.Carriage with CorridorName {CorridorName} already exists.", command.CorridorName);
                return RouteSegmentErrors.CorridorNameAlreadyExists;
            }


            var createRouteSegmentResult = Masar.Domain.RouteSegments.RouteSegment.Create(
                Guid.NewGuid(), command.FromStationId, command.ToStationId, command.TrackType,
                command.DistanceKm, command.EstPassengerTimeMin, command.CorridorName);

            if(createRouteSegmentResult.IsError)
            {
                return createRouteSegmentResult.Errors;
            }

            var routeSegment = createRouteSegmentResult.Value;

            await _context.RouteSegments.AddAsync(routeSegment);

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("RouteSegment with id {RouteSegmentId} created successfully.", routeSegment.Id);
            return routeSegment.ToDto();
        }
    }
}
