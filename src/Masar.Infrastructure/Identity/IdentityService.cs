using System;
using System.Linq;
using System.Threading.Tasks;
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Identity.Dtos;
using Masar.Domain.Common.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Masar.Infrastructure.Identity;

public class IdentityService(
    UserManager<AppUser> userManager,
    IUserClaimsPrincipalFactory<AppUser> userClaimsPrincipalFactory,
    IAuthorizationService authorizationService) : IIdentityService
{
    private readonly UserManager<AppUser> _userManager = userManager;
    private readonly IUserClaimsPrincipalFactory<AppUser> _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService = authorizationService;

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string? policyName)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName!);

        return result.Succeeded;
    }

    public async Task<Result<AppUserDto>> AuthenticateAsync(string userName, string password)
    {
        // 1. البحث باستخدام اسم المستخدم بدلاً من الإيميل
        var user = await _userManager.FindByNameAsync(userName);

        if (user is null)
        {
            return Error.NotFound("User_Not_Found", $"User with username '{userName}' not found");
        }

        // 2. التحقق من كلمة المرور
        if (!await _userManager.CheckPasswordAsync(user, password))
        {
            return Error.Conflict("Invalid_Login_Attempt", "Username / Password are incorrect");
        }

        // 3. إرجاع الـ Dto مع الـ Roles والـ Claims
        return new AppUserDto(
            user.Id,
            user.UserName!,
            await _userManager.GetRolesAsync(user),
            await _userManager.GetClaimsAsync(user));
    }

    public async Task<Result<AppUserDto>> GetUserByIdAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new InvalidOperationException(nameof(userId));

        var roles = await _userManager.GetRolesAsync(user);

        var claims = await _userManager.GetClaimsAsync(user);

        return new AppUserDto(user.Id, user.UserName!, roles, claims);
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return user?.UserName;
    }

    public async Task<Result<string>> CreateUserAsync(string userId, string userName, string password)
    {
        var appUser = new AppUser
        {
            Id = userId,
            UserName = userName,
            Email = $"{userName.ToLower()}@masar.local", // إيميل افتراضي لترضية شرط Identity
            EmailConfirmed = true
        };

        var result = await _userManager.CreateAsync(appUser, password);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Error.Conflict("Identity_Create_Failed", errors);
        }

        return appUser.Id;
    }

    public async Task<Result<bool>> UpdatePasswordAsync(string userId, string newPassword)
    {
        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser is null)
        {
            return Error.NotFound("Identity_User_NotFound", "Identity user not found.");
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
        var result = await _userManager.ResetPasswordAsync(appUser, token, newPassword);

        if (!result.Succeeded)
        {
            var errors = string.Join("; ", result.Errors.Select(e => e.Description));
            return Error.Conflict("Identity_Password_Update_Failed", errors);
        }

        return true;
    }

    public async Task<Result<bool>> DeleteUserAsync(string userId)
    {
        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser is null) return true;

        var result = await _userManager.DeleteAsync(appUser);

        if (!result.Succeeded)
        {
            return Error.Failure("Identity_Delete_Failed", "Could not remove identity user.");
        }

        return true;
    }
}