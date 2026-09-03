using Masar.Application.Common.Interfaces;
using Masar.Application.Features.RouteSegments.Mappers;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Masar.Application.Features.RouteSegments.Commands.UpdateRouteSegment
{
    public class UpdateRouteSegmentCommandHandler(IAppDbContext context, ILogger<UpdateRouteSegmentCommandHandler> logger) : IRequestHandler<UpdateRouteSegmentCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;

        private readonly ILogger<UpdateRouteSegmentCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdateRouteSegmentCommand request, CancellationToken cancellationToken)
        {
            var routeSegment = await _context.RouteSegments.FirstOrDefaultAsync(x => x.Id == request.RouteSegmentID, cancellationToken);
            if (routeSegment is null)
            {
                _logger.LogWarning("RouteSegment update aborted. RouteSegment with id {RouteSegmentId} not found.", request.RouteSegmentID);
                return RouteSegmentErrors.RouteSegmentNotFound;
            }

            if (!(await _context.Stations.AnyAsync(x => x.Id == request.FromStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment update aborted.FromStation with id {FromStationId} not found.", request.FromStationId);
                return RouteSegmentErrors.FromStationIdNotFound;
            }

            if (!(await _context.Stations.AnyAsync(x => x.Id == request.ToStationId, cancellationToken)))
            {
                _logger.LogWarning("RouteSegment update aborted.ToStation with id {ToStationId} not found.", request.ToStationId);
                return RouteSegmentErrors.ToStationIdNotFound;
            }

            if (await _context.RouteSegments.AnyAsync(x => x.FromStationId == request.FromStationId && x.ToStationId == request.ToStationId && x.Id != routeSegment.Id, cancellationToken))
            {
                _logger.LogWarning("RouteSegment update aborted.RouteSegment with FromStationId {FromStationId} and ToStationId {ToStationId} already exists.", request.FromStationId, request.ToStationId);
                return RouteSegmentErrors.RouteSegmentAlreadyExists;
            }

            if (await _context.RouteSegments.AnyAsync(x => x.CorridorName == request.CorridorName && x.Id != routeSegment.Id, cancellationToken))
            {
                _logger.LogWarning("RouteSegment update aborted.Carriage with CorridorName {CorridorName} already exists.", request.CorridorName);
                return RouteSegmentErrors.CorridorNameAlreadyExists;
            }

            var updateRouteSegmentResult = routeSegment.Update(
                request.FromStationId,
                request.ToStationId,
                request.TrackType,
                request.DistanceKm,
                request.EstPassengerTimeMin,
                request.CorridorName);

            if (updateRouteSegmentResult.IsError)
            {
                return updateRouteSegmentResult.Errors;
            }         

            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("RouteSegment with id {RouteSegmentId} updated successfully.", request.RouteSegmentID);
            return Result.Updated;
        }

    }
}
