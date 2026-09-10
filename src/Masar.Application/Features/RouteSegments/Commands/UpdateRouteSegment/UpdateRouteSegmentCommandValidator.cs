using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteSegments.Commands.UpdateRouteSegment
{
    public sealed class UpdateRouteSegmentCommandValidator : AbstractValidator<UpdateRouteSegmentCommand>
    {
        public UpdateRouteSegmentCommandValidator() 
        {
            RuleFor(x => x.FirstStationId)
                .NotEmpty()
                .WithMessage("FirstStationId is required.");

            RuleFor(x => x.SecondStationId)
            .NotEmpty()
            .WithMessage("SecondStationId is required.");

            RuleFor(x => x.TrackType)
                .IsInEnum()
                .WithMessage("Track type is invalid.");

            RuleFor(x => x.DistanceKm)
                .InclusiveBetween(1m, 9999.99m)
                .WithMessage("Distance Km must be between 1 and 9999.99 km.");

            RuleFor(x => x.EstPassengerTimeMin)
                .GreaterThan(0)
                .WithMessage("EstPassengerTimeMin must be greater than 0.");

            RuleFor(x => x.CorridorName)
                .NotEmpty()
                .WithMessage("Corridor Name is required.")
                .MaximumLength(100)
                .WithMessage("Corridor Name must not exceed 100 characters.");

            RuleFor(x => x.FirstStationId)
                .NotEqual(x => x.SecondStationId)
                .WithMessage("The origin station (FirstStationId) and destination station (SecondStationId) must be different");

        }
    }
}
