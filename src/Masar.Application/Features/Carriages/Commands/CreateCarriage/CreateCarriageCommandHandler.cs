using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Carriages.Mappers;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.CreateCarriage
{
    public class CreateCarriageCommandHandler(IAppDbContext context) : IRequestHandler<CreateCarriageCommand, Result<CarriageDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<CarriageDto>> Handle(CreateCarriageCommand command, CancellationToken cancellationToken)
        {
            if(_context.Carriages.Any(x => x.CarriageNumber == command.CarriageNumber))
            {
                return CarriageErrors.CarriageNumberAlreadyExists;
            }

            if(!_context.Trains.Any(x => x.Id == command.TrainId))
            {
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

            return carriage.ToDto();
        }


    }
}
