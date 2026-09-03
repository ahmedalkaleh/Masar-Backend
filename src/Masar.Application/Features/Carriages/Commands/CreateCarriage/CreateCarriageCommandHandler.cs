using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Carriages.Mappers;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;


namespace Masar.Application.Features.Carriages.Commands.CreateCarriage
{
    public class CreateCarriageCommandHandler(IAppDbContext context, ILogger logger) : IRequestHandler<CreateCarriageCommand, Result<CarriageDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger _logger = logger;

        public async Task<Result<CarriageDto>> Handle(CreateCarriageCommand command, CancellationToken cancellationToken)
        {
            if(_context.Carriages.Any(x => x.CarriageNumber == command.CarriageNumber))
            {
                _logger.LogWarning("Carriage Creation aborted.Carriage with number {CarriageNumber} already exists.", command.CarriageNumber);
                return CarriageErrors.CarriageNumberAlreadyExists;
            }

            if(!_context.Trains.Any(x => x.Id == command.TrainId))
            {
                _logger.LogWarning("Carriage Creation aborted.Train with id {TrainId} not found.", command.TrainId);
                return CarriageErrors.TrainNotFound;
            }

            var createCarriageResult = Masar.Domain.Carriages.Carriage.Create(Guid.NewGuid(), command.TrainId, command.CarriageNumber, command.ClassType, command.TotalSeats);

            if(createCarriageResult.IsError)
            {
                return createCarriageResult.Errors;
            }

            await _context.Carriages.AddAsync(createCarriageResult.Value);

            await _context.SaveChangesAsync(cancellationToken);

            var carriage = createCarriageResult.Value;
            _logger.LogInformation("Carriage with id {CarriageId} created successfully.", carriage.Id);
            return carriage.ToDto();
        }


    }
}
