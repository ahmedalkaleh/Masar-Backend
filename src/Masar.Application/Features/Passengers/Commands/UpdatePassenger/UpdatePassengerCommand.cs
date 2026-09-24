using Masar.Application.Features.Passengers.Dtos;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Commands.UpdatePerson;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.UpdatePassenger
{
    public sealed record UpdatePassengerCommand(
        Guid PassengerID,
        CreatePersonCommand Person
        ) : IRequest<Result<Updated>>
    {
    }
}
