using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.CreateSeat
{
    public sealed record CreateSeatCommand : IRequest<Result<SeatDto>>
    {
        /// <summary>
        /// The carriage to which the seat belongs.
        /// </summary>
        public Guid CarriageId { get; init; }

        /// <summary>
        /// The row number of the seat.
        /// Maximum length: 2 characters.
        /// </summary>
        public string RowNumber { get; init; } = null!;

        /// <summary>
        /// The column number of the seat.
        /// Length: between 1 and 6
        /// </summary>
        public byte ColumnNumber { get; init; }

        /// <summary>
        /// The type of the seat.
        /// 0 = Normal, 1 = VIP.
        /// </summary>
        public SeatType SeatType { get; init; }
    }
}
