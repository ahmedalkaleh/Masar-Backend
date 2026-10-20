using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetWithSeatesById;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Queries.GetCarriagesWithSeatesByTripId
{
    public sealed record GetTrainByTripIdQuery(Guid TripId) : IRequest<Result<TrainWithCarriagesSeatsDto>>;

}
