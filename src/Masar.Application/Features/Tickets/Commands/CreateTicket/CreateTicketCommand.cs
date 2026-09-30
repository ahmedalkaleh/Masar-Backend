using Masar.Application.Features.Tickets.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketCommand : IRequest<Result<TicketDto>>
    {
        /// <summary>
        /// The seat assigned to the ticket.
        /// </summary>
        public Guid SeatId { get; init; }

        /// <summary>
        /// The full name of the passenger assigned to the ticket.
        /// Maximum length: 100 characters.
        /// </summary>
        public string Fullname { get; init; } = null!;
    }
}
