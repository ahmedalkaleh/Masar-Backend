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

            RuleFor(x => x.Fullname)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MinimumLength(2).WithMessage("Full name must be at least 2 characters.")
                .MaximumLength(150).WithMessage("Full name must not exceed 150 characters.");
        }
    }
}
