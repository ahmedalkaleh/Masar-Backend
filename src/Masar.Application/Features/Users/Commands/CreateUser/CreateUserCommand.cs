using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Users.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Identity;
using MediatR;

namespace Masar.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand : IRequest<Result<UserDto>>
    {
        /// <summary>
        /// The person associated with the user account.
        /// </summary>
        public Guid PersonId { get; init; }

        /// <summary>
        /// The username used to access the system.
        /// Maximum length: 50 characters.
        /// </summary>
        public string Username { get; init; } = null!;

        /// <summary>
        /// The password used to authenticate the user.
        /// Maximum length: 6 characters.
        /// </summary>
        public string Password { get; init; } = null!;

        /// <summary>
        /// The role assigned to the user.
        /// 0 = StationEmployee, 1 = Manager.
        /// </summary>
        public Role Role { get; init; }
    }
}