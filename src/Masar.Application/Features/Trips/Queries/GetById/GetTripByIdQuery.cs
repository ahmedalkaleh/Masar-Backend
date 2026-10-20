using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetById;
using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Queries.GetById
{
    public sealed record GetTripByIdQuery(Guid Id) : IRequest<Result<TripDetailsDto>>;

}
