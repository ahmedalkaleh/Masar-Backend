using System;
using System.Threading;
using System.Threading.Tasks;
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Users.Dtos;
using Masar.Application.Features.Users.Mappers;
using Masar.Domain.Common.Results;
using Masar.Domain.Users;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(
        IAppDbContext context,
        IIdentityService identityService,
        ILogger logger) : IRequestHandler<CreateUserCommand, Result<UserDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly IIdentityService _identityService = identityService;
        private readonly ILogger _logger = logger;

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken cancellationToken)
        {
            // 1. التأكد من وجود الشخص (Person)
            var personExists = await _context.Persons.AnyAsync(p => p.Id == command.PersonId, cancellationToken);
            if (!personExists)
            {
                _logger.LogWarning("User creation aborted. Person with ID {PersonId} was not found.", command.PersonId);
                return Error.NotFound("Person_Not_Found", $"Person with ID {command.PersonId} was not found.");
            }

            // 2. التحقق المسبق من عدم وجود مستخدم مرتبط بهذا الشخص
            var userExists = await _context.Users.AnyAsync(u => u.PersonId == command.PersonId, cancellationToken);
            if (userExists)
            {
                _logger.LogWarning("User creation aborted. A user account already exists for the person with ID {PersonId}.", command.PersonId);
                return Error.Conflict("User_Already_Exists", "A user account already exists for this person.");
            }

            // 3. إنشاء كيان الـ Domain والتحقق منه
            var newGuid = Guid.NewGuid();
            var createUserResult = User.Create(
                newGuid,
                command.PersonId,
                command.Username.Trim(),
                command.Role,
                false
            );

            if (createUserResult.IsError)
            {
                return createUserResult.Errors;
            }

            // 4. إنشاء المستخدم في Identity عبر IIdentityService بدلاً من UserManager المباشر
            var identityResult = await _identityService.CreateUserAsync(
                newGuid.ToString(),
                command.Username.Trim(),
                command.Password);

            if (identityResult.IsError)
            {
                return identityResult.Errors;
            }

            // 5. حفظ الـ Domain User في قاعدة البيانات
            try
            {
                _context.Users.Add(createUserResult.Value);
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch
            {
                _logger.LogWarning("Failed to save the new user with ID {UserId} to the database. Rolling back Identity creation.", newGuid);
                await _identityService.DeleteUserAsync(newGuid.ToString());
                throw;
            }
            
            _logger.LogInformation("User with ID {UserId} created successfully.", newGuid);
            return createUserResult.Value.ToDto();
        }
    }
}