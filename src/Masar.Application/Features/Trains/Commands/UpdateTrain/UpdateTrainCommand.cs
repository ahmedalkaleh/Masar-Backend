using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Commands.UpdateTrain
{
    public sealed record UpdateTrainCommand
    (
        Guid TrainID,
        string Code,
        string Name,
        string TrainType,
        int MaxSpeedKmh
   ) : IRequest<Result<Updated>>;
}
