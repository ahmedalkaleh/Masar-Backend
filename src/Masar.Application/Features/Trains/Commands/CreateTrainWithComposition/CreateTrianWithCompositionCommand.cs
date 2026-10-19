
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using MediatR;

namespace Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;

public sealed partial record CreateTrainWithCompositionCommand : IRequest<Result<TrainDto>>
{
    public string Code { get; init; } = null!;
    public string Name { get; init; } = null!;
    public string TrainType { get; init; } = null!;
    public int MaxSpeedKmh { get; init; }
    public Guid CurrentStationId { get; init; }

    public List<CreateCarriageCompositionCommand> Carriages { get; init; } = [];
}
