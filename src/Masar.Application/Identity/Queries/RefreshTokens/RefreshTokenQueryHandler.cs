using System.Security.Claims;

using Masar.Application.Common.Errors;
using Masar.Application.Common.Interfaces;
using Masar.Domain.Common.Results;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Identity.Queries.RefreshTokens;

public class RefreshTokenQueryHandler(IIdentityService identityService, IAppDbContext context, ITokenProvider tokenProvider, ILogger<RefreshTokenQueryHandler>  logger)
    : IRequestHandler<RefreshTokenQuery, Result<TokenResponse>>
{
   
    private readonly IIdentityService _identityService = identityService;
    private readonly IAppDbContext _context = context;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ILogger<RefreshTokenQueryHandler> _logger = logger;

    public async Task<Result<TokenResponse>> Handle(RefreshTokenQuery request, CancellationToken ct)
    {
        var principal = _tokenProvider.GetPrincipalFromExpiredToken(request.ExpiredAccessToken);

        if (principal is null)
        {
            _logger.LogWarning("Token refresh failed. Expired access token is invalid.");
            return ApplicationErrors.ExpiredAccessTokenInvalid;
        }

        var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId is null)
        {
            _logger.LogWarning("Token refresh failed. User ID claim is invalid.");
            return ApplicationErrors.UserIdClaimInvalid;
        }

        var getUserResult = await _identityService.GetUserByIdAsync(userId);

        if (getUserResult.IsError)
        {
            return getUserResult.Errors;
        }

        var refreshToken = await _context.RefreshTokens.FirstOrDefaultAsync(r => r.Token == request.RefreshToken && r.UserId == userId, ct);

        if (refreshToken is null || refreshToken.ExpiresOnUtc < DateTime.UtcNow)
        {
            _logger.LogWarning("Token refresh failed. Refresh token is invalid or expired for user ID {UserId}.", userId);
            return ApplicationErrors.RefreshTokenExpired;
        }

        var generateTokenResult = await _tokenProvider.GenerateJwtTokenAsync(getUserResult.Value, ct);

        if (generateTokenResult.IsError)
        {
          
            return generateTokenResult.Errors;
        }
        _logger.LogInformation("Token refresh successful for user ID {UserId}.", userId);
        return generateTokenResult.Value;
    }
}