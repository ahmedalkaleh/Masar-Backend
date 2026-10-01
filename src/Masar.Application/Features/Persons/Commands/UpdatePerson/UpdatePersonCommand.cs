using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Masar.Domain.Common.Results;
namespace Masar.Application.Features.Persons.Commands.UpdatePerson
{
    public sealed record UpdatePersonCommand : IRequest<Result<Updated>>
    {
        /// <summary>
        /// The person to be updated.
        /// </summary>
        public Guid PersonID { get; init; }

        /// <summary>
        /// The full name of the person.
        /// Maximum length: 150 characters.
        /// </summary>
        public string FullName { get; init; } = null!;

        /// <summary>
        /// The email address of the person.
        /// Maximum length: 150 characters.
        /// </summary>
        public string Email { get; init; } = null!;

        /// <summary>
        /// The phone number of the person.
        /// Phone number must be 7–15 digits and may start with '+'.
        /// </summary>
        public string PhoneNumber { get; init; } = null!;
    }
}
