using Masar.Application.Features.Identity.Dtos;
using Masar.Domain.Common.Results;

namespace Masar.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string? policyName);

    Task<Result<AppUserDto>> AuthenticateAsync(string UserName, string password);

    Task<Result<AppUserDto>> GetUserByIdAsync(string userId);

    Task<string?> GetUserNameAsync(string userId);
    Task<Result<string>> CreateUserAsync(string userId, string userName, string password);
    Task<Result<bool>> UpdatePasswordAsync(string userId, string newPassword);
    Task<Result<bool>> DeleteUserAsync(string userId);
}