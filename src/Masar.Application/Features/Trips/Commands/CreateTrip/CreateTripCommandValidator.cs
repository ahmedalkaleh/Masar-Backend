using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Commands.CreateTrip
{
    public class CreateTripCommandValidator:AbstractValidator<CreateTripCommand>
    {
        public CreateTripCommandValidator()
        {
            RuleFor(x => x.TrainId).NotEmpty().WithMessage("TrainId is required.");
            RuleFor(x => x.RoutTemplateId).NotEmpty().WithMessage("RoutTemplateId is required.");
            RuleFor(x => x.DepartureTime).NotEmpty().WithMessage("DepartureTime is required.").GreaterThan(DateTime.UtcNow).WithMessage("DepartureTime must be in the future.");
        }
    }
}
