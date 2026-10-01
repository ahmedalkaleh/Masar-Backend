using FluentValidation;
using Masar.Application.Features.Carriages.Commands.CreateCarriage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.UpdateCarriage
{
    public sealed class UpdateCarriageCommandValidator : AbstractValidator<UpdateCarriageCommand>
    {
        public UpdateCarriageCommandValidator()
        {
            RuleFor(x => x.CarriageID)
            .NotEmpty()
            .WithMessage("Carriage ID is required.");

            RuleFor(x => x.TrainId)
            .NotEmpty()
            .WithMessage("Train ID is required.");

            RuleFor(x => x.CarriageNumber)
                .GreaterThan(0)
                .WithMessage("Carriage number must be greater than 0.");

            RuleFor(x => x.ClassType)
                .IsInEnum()
                .WithMessage("Seat type is invalid.");

            RuleFor(x => x.TotalSeats)
                .InclusiveBetween((short)1, (short)500)
                .WithMessage("Total seats must be between 1 and 500.");
        }
    }
}
