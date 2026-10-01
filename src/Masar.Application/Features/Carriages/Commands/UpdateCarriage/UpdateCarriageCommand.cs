using Masar.Application.Features.Carriages.Dtos;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.UpdateCarriage
{
    public sealed record UpdateCarriageCommand : IRequest<Result<Updated>>
    {
        /// <summary>
        /// The carriage to be updated.
        /// </summary>
        public Guid CarriageID { get; init; }

        /// <summary>
        /// The train to which the carriage belongs.
        /// </summary>
        public Guid TrainId { get; init; }

        /// <summary>
        /// The number assigned to the carriage within the train.
        /// Must be greater than 0.
        /// </summary>
        public int CarriageNumber { get; init; }

        /// <summary>
        /// The class type of the carriage.
        /// Length: Between 2 and 50.
        /// </summary>
        public ClassType ClassType { get; init; }

        /// <summary>
        /// The total number of seats available in the carriage.
        /// Must be Between 1 and 500
        /// </summary>
        public short TotalSeats { get; init; }
    }
}
