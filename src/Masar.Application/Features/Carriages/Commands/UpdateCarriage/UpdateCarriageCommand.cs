using Masar.Application.Features.Carriages.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Commands.UpdateCarriage
{
    public sealed record UpdateCarriageCommand(
        Guid CarriageID,
        Guid TrainId,
        int CarriageNumber,
        string ClassType,
        int TotalSeats) : IRequest<Result<Updated>>
    {
    }
}
