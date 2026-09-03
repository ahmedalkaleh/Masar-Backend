using Masar.Application.Common.Interfaces;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Carriages.Commands.UpdateCarriage
{
    public class UpdateCarriageCommandHandler(IAppDbContext context, ILogger logger) : IRequestHandler<UpdateCarriageCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger _logger = logger;

        public async Task<Result<Updated>> Handle(UpdateCarriageCommand request, CancellationToken cancellationToken)
        {
            var carriage = await _context.Carriages.FirstOrDefaultAsync(x => x.Id == request.CarriageID, cancellationToken);
            if (carriage is null)
            {
                _logger.LogWarning("Carriage update aborted. Carriage with id {CarriageId} not found.", request.CarriageID);
                return CarriageErrors.CarriageNotFound;
            }

            if (!_context.Trains.Any(x => x.Id == request.TrainId))
            {
                _logger.LogWarning("Carriage update aborted. Train with id {TrainId} not found.", request.TrainId);
                return CarriageErrors.TrainNotFound;
            }
            
            if (_context.Carriages.Any(x => x.CarriageNumber == request.CarriageNumber && carriage.CarriageNumber != request.CarriageNumber))
            {
                _logger.LogWarning("Carriage update aborted. Carriage number {CarriageNumber} already exists.", request.CarriageNumber);
                return CarriageErrors.CarriageNumberAlreadyExists;
            }

            var updatedCarriageResult = carriage.Update(

                request.TrainId,
                request.CarriageNumber,
                request.ClassType,
                request.TotalSeats);

            if (updatedCarriageResult.IsError)
            {
                return updatedCarriageResult.Errors;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return Result.Updated;

        }
    }
    
}
