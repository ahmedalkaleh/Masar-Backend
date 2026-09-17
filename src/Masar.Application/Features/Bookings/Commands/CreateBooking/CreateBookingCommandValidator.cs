using FluentValidation;
using Masar.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandValidator : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingCommandValidator() 
        {
            RuleFor(x => x.PassengerId).NotEmpty().WithMessage("PassengerId is required.");
            RuleFor(x => x.TripId).NotEmpty().WithMessage("TripId is required.");
            RuleFor(x => x.BoardingStationId).NotEmpty().WithMessage("BoardingStationId is required.");
            RuleFor(x => x.AlightingStationId).NotEmpty().WithMessage("AlightingStationId is required.");

            RuleFor(x => x.Tickets)
                        .NotNull()
                        .WithState(_ => BookingErrors.TicketsListRequired)
                        // 2. التحقق من أنها تحتوي على عنصر واحد على الأقل (تنفذ فقط إذا لم تكن Null)
                        .Must(tickets => tickets != null && tickets.Any())
                        .WithState(_ => BookingErrors.AtLeastOneTicketRequired);

        }

    }
}
