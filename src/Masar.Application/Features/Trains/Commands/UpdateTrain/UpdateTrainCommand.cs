using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Commands.UpdateTrain
{
    public sealed record UpdateTrainCommand : IRequest<Result<Updated>>
    {
        /// <summary>
        /// The train to be updated.
        /// </summary>
        public Guid TrainID { get; init; }

        /// <summary>
        /// The unique code assigned to the train.
        /// Length: between 2 and 20 characters.
        /// </summary>
        public string Code { get; init; } = null!;

        /// <summary>
        /// The name of the train.
        /// Length: between 2 and 100 characters.
        /// </summary>
        public string Name { get; init; } = null!;

        /// <summary>
        /// The type of the train.
        /// Length: between 2 and 50 characters.
        /// </summary>
        public string TrainType { get; init; } = null!;

        /// <summary>
        /// The maximum speed of the train in kilometers per hour.
        /// Must be between 1 and 500.
        /// </summary>
        public int MaxSpeedKmh { get; init; }
    }
}
