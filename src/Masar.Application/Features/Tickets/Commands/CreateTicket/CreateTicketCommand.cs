using Masar.Application.Features.Tickets.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Tickets.Commands.CreateTicket
{
    public sealed record CreateTicketCommand(
    Guid SeatId,
    string Fullname) : IRequest<Result<TicketDto>>
    {
    }
}
