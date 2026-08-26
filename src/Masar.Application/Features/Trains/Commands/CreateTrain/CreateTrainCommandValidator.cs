using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Commands.CreateTrain
{
    public sealed class CreateTrainCommandValidator : AbstractValidator<CreateTrainCommand>
    {
        public CreateTrainCommandValidator()
        {
            RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required.")
            .MinimumLength(2).WithMessage("Train code must be at least 2 characters.")
            .MaximumLength(20).WithMessage("Train code must not exceed 20 characters.");

            RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MinimumLength(2).WithMessage("Train name must be at least 2 characters.")
            .MaximumLength(100).WithMessage("Train name must not exceed 100 characters.");

            RuleFor(x => x.TrainType)
            .NotEmpty().WithMessage("Train type is required.")
            .MinimumLength(2).WithMessage("Train type must be at least 2 characters.")
            .MaximumLength(50).WithMessage("Train type must not exceed 50 characters.");
        }
    }
}
