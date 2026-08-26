using System;
using System.Collections.Generic;
using System.Linq;
using Masar.Application.Features.Users.Dtos;
using Masar.Domain.Users;

namespace Masar.Application.Features.Users.Mappers
{
    public static class UserMapper
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                UserId = user.Id,
                PersonId = user.PersonId,
                Username = user.Username,
                Role = user.Role,
                IsDelete = user.IsDelete
            };
        }

        public static List<UserDto> ToDtos(this IEnumerable<User> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}