
using FluentValidation;

namespace Masar.Application.Features.Seats.Queries.GetAvailableSeats;

public sealed class GetAvailableSeatsQueryValidator
    : AbstractValidator<GetAvailableSeatsQuery>
{
    public GetAvailableSeatsQueryValidator()
    {
        RuleFor(x => x.TripId)
            .NotEmpty();

        RuleFor(x => x.BoardingStationId)
            .NotEmpty();

        RuleFor(x => x.AlightingStationId)
            .NotEmpty()
            .NotEqual(x => x.BoardingStationId)
            .WithMessage(
                "Boarding station and alighting station must be different.");
    }
}
