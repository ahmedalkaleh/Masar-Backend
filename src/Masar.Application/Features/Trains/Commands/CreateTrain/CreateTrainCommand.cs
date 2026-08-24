using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Commands.CreateTrain
{
    public sealed record CreateTrainCommand(
        string Code,
        string Name,
        string TrainType,
        int MaxSpeedKmh,
        Guid CurrentStationId) : IRequest<Result<TrainDto>>
    {
    }
}
