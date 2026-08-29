using Masar.Application.Common.Interfaces;
using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.UpdateStation
{
    public class UpdateStationCommandHandler(IAppDbContext context) : IRequestHandler<UpdateStationCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<Updated>> Handle(UpdateStationCommand request, CancellationToken cancellationToken)
        {
            var station = await _context.Stations.FirstOrDefaultAsync(x => x.Id == request.StationID, cancellationToken);
            if (station is null)
            {
                return StationErrors.StationNotFound;
            }

            var updatedStationResult = station.Update(
                request.NameAr, request.NameEn, request.Type, request.Latitude,
                request.Longitude, request.Governorate, request.CustomsDelayMinutes);

            if(updatedStationResult.IsError)
            {
                return updatedStationResult.Errors;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return Result.Updated;


        }
    }
}
