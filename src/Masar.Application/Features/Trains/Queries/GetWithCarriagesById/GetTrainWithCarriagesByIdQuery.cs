using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetById;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Queries.GetWithCarriagesById
{
    public sealed record GetTrainWithCarriagesByIdQuery(Guid Id) : IRequest<Result<TrainWithCarriagesDto>>;
}
