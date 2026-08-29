using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.UpdateStation
{
    public sealed class UpdateStationCommandValidator : AbstractValidator<UpdateStationCommand>
    {
        public UpdateStationCommandValidator() 
        {
            RuleFor(x => x.NameAr)
            .NotEmpty()
            .WithMessage("Arabic station name is required.")
            .MaximumLength(100)
            .WithMessage("Arabic station name must not exceed 100 characters.");

            RuleFor(x => x.NameEn)
                .NotEmpty()
                .WithMessage("English station name is required.")
                .MaximumLength(100)
                .WithMessage("English station name must not exceed 100 characters.");

            RuleFor(x => x.Latitude)
                .InclusiveBetween(-90, 90)
                .WithMessage("Latitude must be between -90 and 90.");

            RuleFor(x => x.Longitude)
                .InclusiveBetween(-180, 180)
                .WithMessage("Longitude must be between -180 and 180.");

            RuleFor(x => x.Governorate)
                .NotEmpty()
                .WithMessage("Governorate is required.")
                .MaximumLength(100)
                .WithMessage("Governorate must not exceed 100 characters.");

            RuleFor(x => x.CustomsDelayMinutes)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Customs delay minutes cannot be negative.");
        }
    }
}
