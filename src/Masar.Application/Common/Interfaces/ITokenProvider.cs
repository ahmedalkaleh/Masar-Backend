using System.Security.Claims;

using Masar.Application.Features.Identity;
using Masar.Application.Features.Identity.Dtos;
using Masar.Domain.Common.Results;

namespace Masar.Application.Common.Interfaces;

public interface ITokenProvider
{
    Task<Result<TokenResponse>> GenerateJwtTokenAsync(AppUserDto user, CancellationToken ct = default);

    ClaimsPrincipal? GetPrincipalFromExpiredToken(string token);
}