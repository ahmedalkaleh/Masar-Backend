using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Application.Features.RouteTemplates.Queries.GetAll;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Queries.GetById
{
    public sealed record GetTrainByIdQuery(Guid Id) : IRequest<Result<TrainDetailsDto>>;
}
