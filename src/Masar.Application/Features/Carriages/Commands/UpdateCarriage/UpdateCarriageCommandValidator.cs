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
                .NotEmpty()
                .WithMessage("Class type is required.")
                .MinimumLength(2)
                .WithMessage("Class type must be at least 2 characters.")
                .MaximumLength(50)
                .WithMessage("Class type must not exceed 50 characters.");

            RuleFor(x => x.TotalSeats)
                .InclusiveBetween((short)1, (short)500)
                .WithMessage("Total seats must be between 1 and 500.");
        }
    }
}
