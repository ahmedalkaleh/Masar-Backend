using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.UpdateSeat
{
    public sealed record UpdateSeatCommand : IRequest<Result<Updated>>
    {
        /// <summary>
        /// The seat to be updated.
        /// </summary>
        public Guid SeatID { get; init; }

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

        /// <summary>
        /// Indicates whether the seat is currently active.
        /// </summary>
        public bool isActive { get; init; }
    }
}
