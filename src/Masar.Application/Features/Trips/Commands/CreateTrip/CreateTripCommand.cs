using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Commands.CreateTrip
{
    public record CreateTripCommand : IRequest<Result<TripDto>>
    {
        /// <summary>
        /// The train assigned to the trip.
        /// </summary>
        public Guid TrainId { get; init; }

        /// <summary>
        /// The route template used for the trip.
        /// </summary>
        public Guid RoutTemplateId { get; init; }

        /// <summary>
        /// The scheduled departure date and time of the trip.
        /// Must be later than the current date and time.
        /// </summary>
        public DateTime DepartureTime { get; init; }
    }
}
