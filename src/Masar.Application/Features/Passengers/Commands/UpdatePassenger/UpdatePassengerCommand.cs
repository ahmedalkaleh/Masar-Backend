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
    public sealed record UpdatePassengerCommand : IRequest<Result<Updated>>
    {
        /// <summary>
        /// The passenger to be updated.
        /// </summary>
        public Guid PassengerID { get; init; }

        /// <summary>
        /// The personal information of the passenger.
        /// </summary>
        public CreatePersonCommand Person { get; init; } = null!;
    }
}
