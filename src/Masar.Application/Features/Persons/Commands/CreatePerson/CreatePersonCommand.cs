using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Common.Results;
using MediatR;

using Masar.Application.Features.Person.Dtos;

namespace Masar.Application.Features.Persons.Commands.CreatePerson
{
    public sealed record CreatePersonCommand(
        string FullName,
        string Email,
        string PhoneNumber) : IRequest<Result<PersonDto>>
    {
    }
}
