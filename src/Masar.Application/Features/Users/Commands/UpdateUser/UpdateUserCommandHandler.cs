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

namespace Masar.Application.Features.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(
        IAppDbContext context,
        IIdentityService identityService) : IRequestHandler<UpdateUserCommand, Result<UserDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly IIdentityService _identityService = identityService;

        public async Task<Result<UserDto>> Handle(UpdateUserCommand command, CancellationToken cancellationToken)
        {
            // 1. جلب المستخدم من الـ Domain
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == command.Id, cancellationToken);

            if (user is null)
            {
                return Error.NotFound("User_Not_Found", $"User with ID {command.Id} was not found.");
            }

            // 2. التحقق من وجود الشخص المرتبط إذا تم تغييره
            var personExists = await _context.Persons
                .AnyAsync(p => p.Id == command.PersonId, cancellationToken);

            if (!personExists)
            {
                return Error.NotFound("Person_Not_Found", $"Person with ID {command.PersonId} was not found.");
            }

            // 3. تحديث كلمة المرور في Identity فقط إذا تم إرسال كلمة سر جديدة
            if (!string.IsNullOrWhiteSpace(command.NewPassword))
            {
                var updatePasswordResult = await _identityService.UpdatePasswordAsync(user.Id.ToString(), command.NewPassword);
                if (updatePasswordResult.IsError)
                {
                    return updatePasswordResult.Errors;
                }
            }

            // 4. تحديث بيانات كيان الـ Domain
            var updateUserResult = user.Update(
                command.PersonId,
                command.Username.Trim(),
                command.Role,
                command.IsDelete
            );

            if (updateUserResult.IsError)
            {
                return updateUserResult.Errors;
            }

            // 5. حفظ التعديلات في قاعدة البيانات
            await _context.SaveChangesAsync(cancellationToken);

            return user.ToDto();
        }
    }
}