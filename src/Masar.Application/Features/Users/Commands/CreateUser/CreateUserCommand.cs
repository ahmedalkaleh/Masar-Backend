using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Users.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Identity;
using MediatR;

namespace Masar.Application.Features.Users.Commands.CreateUser
{
    public sealed record CreateUserCommand(
        Guid PersonId,
        string Username,
        string Password,
        Role Role) : IRequest<Result<UserDto>>
    {
    }
}