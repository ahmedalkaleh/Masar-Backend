using Masar.Application.Features.Trips.Dtos;
using Masar.Application.Features.Trips.Queries.GetById;
using Masar.Domain.Common.Results;
using Masar.Domain.Trips;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Queries.GetAll
{
    public sealed record GetAllTripsQuery : IRequest<Result<List<TripSummaryDto>>>;

}
