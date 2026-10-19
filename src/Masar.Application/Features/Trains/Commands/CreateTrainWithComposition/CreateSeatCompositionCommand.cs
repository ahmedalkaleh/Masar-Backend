using Masar.Domain.Seats;

namespace Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;

public sealed partial record CreateTrainWithCompositionCommand
{
    public sealed record CreateSeatCompositionCommand
    {
        public string RowNumber { get; init; } = null!;
        public byte ColumnNumber { get; init; }
        public SeatType SeatType { get; init; }
    }
}
