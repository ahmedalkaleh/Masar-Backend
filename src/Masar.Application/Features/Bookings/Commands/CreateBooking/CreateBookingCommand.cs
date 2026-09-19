using Masar.Application.Features.Bookings.Dtos;
using Masar.Application.Features.Tickets.Commands.CreateTicket;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Bookings.Commands.CreateBooking
{
    public sealed record CreateBookingCommand(
    Guid PassengerId,
    Guid TripId,
    Guid BoardingStationId,
    Guid AlightingStationId,
    List<CreateTicketCommand> Tickets) : IRequest<Result<BookingDto>>
    {
    }
}
