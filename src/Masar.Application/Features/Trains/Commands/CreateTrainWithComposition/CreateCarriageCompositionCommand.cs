using Masar.Domain.Carriages;

namespace Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;

public sealed partial record CreateTrainWithCompositionCommand
{
    public sealed record CreateCarriageCompositionCommand
    {
        public int CarriageNumber { get; init; }
        public ClassType ClassType { get; init; }

        // Missing Seats means an empty carriage.
        public List<CreateSeatCompositionCommand> Seats { get; init; } = [];
    }
}
