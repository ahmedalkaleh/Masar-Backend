
using FluentValidation;
using static Masar.Application.Features.Trains.Commands.CreateTrainWithComposition
    .CreateTrainWithCompositionCommand;

namespace Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;

public sealed class CreateTrainWithCompositionCommandValidator
    : AbstractValidator<CreateTrainWithCompositionCommand>
{
    public CreateTrainWithCompositionCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(20);

        RuleFor(x => x.Name)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(100);

        RuleFor(x => x.TrainType)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50);

        RuleFor(x => x.MaxSpeedKmh)
            .InclusiveBetween(1, 500);

        RuleFor(x => x.CurrentStationId)
            .NotEmpty();

        RuleFor(x => x.Carriages)
            .NotNull();

        RuleForEach(x => x.Carriages)
            .SetValidator(new CarriageCompositionValidator());

        RuleFor(x => x.Carriages)
            .Must(carriages =>
                carriages == null ||
                carriages.Select(c => c.CarriageNumber).Distinct().Count()
                    == carriages.Count)
            .WithMessage("Carriage numbers must be unique within the train.");
    }

    private sealed class CarriageCompositionValidator
        : AbstractValidator<CreateCarriageCompositionCommand>
    {
        public CarriageCompositionValidator()
        {
            RuleFor(x => x.CarriageNumber)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.ClassType)
                .IsInEnum();

            RuleFor(x => x.Seats)
                .Must(seats => seats == null || seats.Count <= 500)
                .WithMessage("A carriage cannot contain more than 500 seats.");

            RuleForEach(x => x.Seats)
                .SetValidator(new SeatCompositionValidator());

            RuleFor(x => x.Seats)
                .Must(seats => seats == null ||
                    seats.Select(s => (
                        Row: s.RowNumber?.Trim(),
                        s.ColumnNumber
                    )).Distinct().Count() == seats.Count)
                .WithMessage("Seat positions must be unique within a carriage.");
        }
    }

    private sealed class SeatCompositionValidator
        : AbstractValidator<CreateSeatCompositionCommand>
    {
        public SeatCompositionValidator()
        {
            RuleFor(x => x.RowNumber)
                .NotEmpty()
                .MaximumLength(2);

            RuleFor(x => x.ColumnNumber)
                .InclusiveBetween((byte)1, (byte)6);

            RuleFor(x => x.SeatType)
                .IsInEnum();
        }
    }
}
