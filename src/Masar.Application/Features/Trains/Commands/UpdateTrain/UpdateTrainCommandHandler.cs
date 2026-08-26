using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Commands.UpdateTrain;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Masar.Application.Features.Trains.Commands.UpdateTrain
{
    public class UpdateTrainCommandHandler(IAppDbContext context) : IRequestHandler<UpdateTrainCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        public async Task<Result<Updated>> Handle(UpdateTrainCommand request, CancellationToken cancellationToken)
        {
            var Train = await _context.Trains.FirstOrDefaultAsync(p => p.Id == request.TrainID, cancellationToken);
            if (Train is null)
            {
                return TrainErrors.TrainNotFound;
            }

            if (_context.Trains.Any(x => x.Code == request.Code && Train.Code != request.Code))
            {
                return TrainErrors.CodeAlreadyExists;
            }

            var updatedTrainResult = Train.Update(

                request.Code,
                request.Name,
                request.TrainType,
                request.MaxSpeedKmh);

            if (updatedTrainResult.IsError)
            {
                return updatedTrainResult.Errors;
            }

            var updatedTrain = updatedTrainResult.Value;

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Updated;
        }
    }
}
