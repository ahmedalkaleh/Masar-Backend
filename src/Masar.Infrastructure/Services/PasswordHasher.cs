using Masar.Application.Common.Interfaces;
using Masar.Domain.Users;
using Microsoft.AspNetCore.Identity;

namespace Masar.Infrastructure.Services
{
    public class PasswordHasher(IPasswordHasher<User> identityPasswordHasher) : IPasswordHasher
    {
        private readonly IPasswordHasher<User> _identityPasswordHasher = identityPasswordHasher;

        public string HashPassword(string password)
        {
            return _identityPasswordHasher.HashPassword(null!, password);
        }
    }
}