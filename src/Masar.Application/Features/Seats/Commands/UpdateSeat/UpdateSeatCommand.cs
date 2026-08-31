using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.UpdateSeat
{
    public sealed record UpdateSeatCommand(
    Guid SeatID,
    string RowNumber,
    byte ColumnNumber,
    SeatType SeatType,
    bool isActive) : IRequest<Result<Updated>>
    {
    }
}
