using Masar.Application.Features.Stations.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Queries.GetById
{
    public sealed record GetStationByIdQuery(Guid Id) : IRequest<Result<StationDto>>;
}
