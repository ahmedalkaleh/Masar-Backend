using Masar.Application.Features.Carriages.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Common.Results.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.CreateCarriage
{
    public sealed record CreateCarriageCommand(
        Guid TrainId,
        int CarriageNumber,
        string ClassType,
        int TotalSeats) : IRequest<Result<CarriageDto>>
    {
    }
}
