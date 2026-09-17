using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Tickets.Commands.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        public CreateTicketCommandValidator()
        {
            RuleFor(x => x.SeatId).NotEmpty().WithMessage("SeatId is required.");

            RuleFor(x => x.Fullname).NotEmpty().WithMessage("Full name is required.");
        }
    }
}
