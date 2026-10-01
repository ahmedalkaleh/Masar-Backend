using Masar.Application.Features.Passengers.Dtos;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.CreatePassenger
{
    public sealed record CreatePassengerCommand : IRequest<Result<PassengerDto>>
    {
        /// <summary>
        /// The personal information of the passenger.
        /// </summary>
        public CreatePersonCommand Person { get; init; } = null!;
    }
}
