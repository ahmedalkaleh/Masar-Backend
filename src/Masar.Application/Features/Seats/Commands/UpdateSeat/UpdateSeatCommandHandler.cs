using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Persons.Commands.UpdatePerson;
using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.UpdateSeat
{
    public class UpdateSeatCommandHandler(IAppDbContext context, ILogger<UpdatePersonCommandHandler> logger) : IRequestHandler<UpdateSeatCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdatePersonCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdateSeatCommand request, CancellationToken cancellationToken)
        {
            var seat = await _context.Seats.FirstOrDefaultAsync(x => x.Id == request.SeatID, cancellationToken);

            if(seat is null)
            {
                _logger.LogWarning("Seat {SeatID} not found for update.", request.SeatID);

                return SeatErrors.SeatNotFound;
            }

            if(await _context.Seats.AnyAsync(x => x.CarriageId == seat.CarriageId && x.RowNumber == request.RowNumber
            && x.ColumnNumber == request.ColumnNumber && x.Id != seat.Id))
            {
                _logger.LogWarning(
                       "Seat position already exists. CarriageId: {CarriageId}, RowNumber: {RowNumber}, ColumnNumber: {ColumnNumber}.",
                       seat.CarriageId,
                       request.RowNumber,
                       request.ColumnNumber);

                return SeatErrors.SeatPositionAlreadyExists;
            }

            var updateSeatResult = seat.Update(
                request.RowNumber, request.ColumnNumber, request.SeatType, request.isActive);

            if(updateSeatResult.IsError)
            {
                return updateSeatResult.Errors;
            }

            await _context.SaveChangesAsync(cancellationToken);

            return updateSeatResult.Value;
        }
    }
}
