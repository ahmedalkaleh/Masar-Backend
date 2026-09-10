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
            if(!(await _context.Stations.AnyAsync(x => x.Id == command.FirstStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment Creation aborted.FromStation with id {FirstStationId} not found.", command.FirstStationId);
                return RouteSegmentErrors.FirstStationIdNotFound;
            }

            if (!(await _context.Stations.AnyAsync(x => x.Id == command.SecondStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment Creation aborted.SecondStation with id {SecondStationId} not found.", command.SecondStationId);
                return RouteSegmentErrors.SecondStationIdNotFound;
            }

            if (await _context.RouteSegments.AnyAsync(x => x.FirstStationId == command.FirstStationId && x.SecondStationId == command.SecondStationId, cancellationToken))
            {
                _logger.LogWarning("RouteSegment Creation aborted.RouteSegment with FirstStationId {FirstStationId} and SecondStationId {SecondStationId} already exists.", command.FirstStationId , command.SecondStationId);
                return RouteSegmentErrors.RouteSegmentAlreadyExists;
            }


            if (await _context.RouteSegments.AnyAsync(x => x.CorridorName == command.CorridorName,cancellationToken))
            {
                _logger.LogWarning("RouteSegment Creation aborted.Carriage with CorridorName {CorridorName} already exists.", command.CorridorName);
                return RouteSegmentErrors.CorridorNameAlreadyExists;
            }


            var createRouteSegmentResult = Masar.Domain.RouteSegments.RouteSegment.Create(
                Guid.NewGuid(), command.FirstStationId, command.SecondStationId, command.TrackType,
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
