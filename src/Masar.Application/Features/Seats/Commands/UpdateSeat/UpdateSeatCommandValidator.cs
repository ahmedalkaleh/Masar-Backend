using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Commands.UpdateSeat
{
    public sealed class UpdateSeatCommandValidator : AbstractValidator<UpdateSeatCommand>
    {
        public UpdateSeatCommandValidator() 
        {
            RuleFor(x => x.RowNumber)
                .NotEmpty()
                .WithMessage("Row number is required.")
                .MaximumLength(2)
                .WithMessage("Row number must not exceed 2 characters.");

            RuleFor(x => x.SeatType)
                .IsInEnum()
                .WithMessage("Seat type is invalid.");

            RuleFor(x => x.ColumnNumber)
                .InclusiveBetween((byte)1, (byte)6)
                .WithMessage("Column number must be between 1 and 6.");
        }
    }
}
