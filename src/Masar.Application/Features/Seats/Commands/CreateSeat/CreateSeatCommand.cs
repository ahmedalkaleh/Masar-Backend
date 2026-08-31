using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.CreateSeat
{
    public sealed record CreateSeatCommand(
    Guid CarriageId,
    string RowNumber,
    byte ColumnNumber,
    SeatType SeatType) : IRequest<Result<SeatDto>>
    {
    }
}
