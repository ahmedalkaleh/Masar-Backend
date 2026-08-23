using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Masar.Domain.Common.Results;
namespace Masar.Application.Features.Persons.Commands.UpdatePerson
{
    public sealed record UpdatePersonCommand
    (
        Guid PersonID,
        string FullName,
            string Email,
            string PhoneNumber
   ):IRequest<Result<Updated>>;
}
