using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetById;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Queries.GetAll
{
    public sealed record GetAllTrainsQuery : IRequest<Result<List<TrainSummaryDto>>>;
}
