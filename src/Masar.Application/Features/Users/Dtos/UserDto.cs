using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Identity;

namespace Masar.Application.Features.Users.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; }
        public Guid PersonId { get; set; }
        public string Username { get; set; } = string.Empty;
        public Role Role { get; set; }
        public bool IsDelete { get; set; }
    }
}