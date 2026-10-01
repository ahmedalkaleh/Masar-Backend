using Masar.Application.Features.Bookings.Dtos;
using Masar.Application.Features.Tickets.Commands.CreateTicket;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Bookings.Commands.CreateBooking
{
    public sealed record CreateBookingCommand : IRequest<Result<BookingDto>>
    {
        /// <summary>
        /// The passenger making the booking.
        /// </summary>
        public Guid PassengerId { get; init; }

        /// <summary>
        /// The trip for which the booking is made.
        /// </summary>
        public Guid TripId { get; init; }

        /// <summary>
        /// The station where the passenger boards the train.
        /// </summary>
        public Guid BoardingStationId { get; init; }

        /// <summary>
        /// The station where the passenger leaves the train.
        /// </summary>
        public Guid AlightingStationId { get; init; }

        /// <summary>
        /// The tickets included in the booking.
        /// </summary>
        public List<CreateTicketCommand> Tickets { get; init; } = [];
    }
}
