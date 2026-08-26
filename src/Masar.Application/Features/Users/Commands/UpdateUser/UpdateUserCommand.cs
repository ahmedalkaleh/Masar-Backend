using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Users.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Identity;
using MediatR;

namespace Masar.Application.Features.Users.Commands.UpdateUser
{
    public sealed record UpdateUserCommand(
        Guid Id,
        Guid PersonId,
        string Username,
        string? NewPassword,
        Role Role,
        bool IsDelete) : IRequest<Result<UserDto>>
    {
    }
}