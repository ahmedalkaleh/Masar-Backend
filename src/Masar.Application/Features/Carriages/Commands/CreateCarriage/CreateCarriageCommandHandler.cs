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
using Microsoft.EntityFrameworkCore;


namespace Masar.Application.Features.Carriages.Commands.CreateCarriage
{
    public class CreateCarriageCommandHandler(IAppDbContext context, ILogger<CreateCarriageCommandHandler> logger) : IRequestHandler<CreateCarriageCommand, Result<CarriageDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateCarriageCommandHandler> _logger = logger;

        public async Task<Result<CarriageDto>> Handle(CreateCarriageCommand command, CancellationToken cancellationToken)
        {
            if(await _context.Carriages.AnyAsync(x => x.CarriageNumber == command.CarriageNumber && x.TrainId == command.TrainId, cancellationToken))
            {
                _logger.LogWarning("Carriage Creation aborted.Carriage with number {CarriageNumber} already exists for Tran {TrainId}.", command.CarriageNumber, command.TrainId);
                return CarriageErrors.CarriageNumberAlreadyExists;
            }

            if(! await _context.Trains.AnyAsync(x => x.Id == command.TrainId, cancellationToken))
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
