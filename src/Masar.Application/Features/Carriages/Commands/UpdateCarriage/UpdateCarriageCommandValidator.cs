using FluentValidation;
using Masar.Application.Features.Carriages.Commands.CreateCarriage;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.UpdateCarriage
{
    public sealed class UpdateCarriageCommandValidator : AbstractValidator<CreateCarriageCommand>
    {
        public UpdateCarriageCommandValidator()
        {
            RuleFor(x => x.TrainId)
            .NotEmpty()
            .WithMessage("Train ID is required.");

            RuleFor(x => x.CarriageNumber)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Carriage number cannot be negative.");

            RuleFor(x => x.ClassType)
                .NotEmpty()
                .WithMessage("Class type is required.")
                .MinimumLength(2)
                .WithMessage("Class type must be at least 2 characters.")
                .MaximumLength(50)
                .WithMessage("Class type must not exceed 50 characters.");

            RuleFor(x => x.TotalSeats)
                .GreaterThan(0)
                .WithMessage("Total seats must be greater than 0.");
        }
    }
}
