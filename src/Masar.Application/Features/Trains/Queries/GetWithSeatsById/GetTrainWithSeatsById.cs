using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetWithCarriagesById;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Queries.GetWithSeatesById
{
    public sealed record GetTrainWithSeatsByIdQuery(Guid Id) : IRequest<Result<TrainWithSeatsDto>>;
}
