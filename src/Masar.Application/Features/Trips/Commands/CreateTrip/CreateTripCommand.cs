using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Commands.CreateTrip
{
    public record CreateTripCommand(Guid TrainId, Guid RoutTemplateId, DateTime DepartureTime) : IRequest<Result<TripDto>>;
}
