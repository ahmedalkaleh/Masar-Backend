using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Persons.Commands.UpdatePerson;
using Masar.Application.Features.Seats.Dtos;
using Masar.Application.Features.Seats.Mappers;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.CreateSeat
{
    public class CreateSeatCommandHandler(IAppDbContext context, ILogger<UpdatePersonCommandHandler> logger) : IRequestHandler<CreateSeatCommand, Result<SeatDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdatePersonCommandHandler> _logger = logger;

        public async Task<Result<SeatDto>> Handle(CreateSeatCommand command, CancellationToken cancellationToken)
        {
            if(!(await _context.Carriages.AnyAsync(x => x.Id == command.CarriageId, cancellationToken)))
            {
                _logger.LogWarning("Carriage {CarriageId} not found when creating seat.",command.CarriageId);

                return SeatErrors.CarriageNotFound;
            }

            if (await _context.Seats.AnyAsync(x => x.CarriageId == command.CarriageId && x.RowNumber == command.RowNumber
            && x.ColumnNumber == command.ColumnNumber))
            {
                _logger.LogWarning(
                       "Seat position already exists. CarriageId: {CarriageId}, RowNumber: {RowNumber}, ColumnNumber: {ColumnNumber}.",
                       command.CarriageId,
                       command.RowNumber,
                       command.ColumnNumber);

                return SeatErrors.SeatPositionAlreadyExists;
            }

            var createSeatResult = Masar.Domain.Seats.Seat.Create(Guid.NewGuid(), command.CarriageId,
                command.RowNumber, command.ColumnNumber, command.SeatType);

            if(createSeatResult.IsError)
            {
                return createSeatResult.Errors;
            }


            await _context.Seats.AddAsync(createSeatResult.Value);

            await _context.SaveChangesAsync(cancellationToken);

            return createSeatResult.Value.ToDto();
        }
    }
}
