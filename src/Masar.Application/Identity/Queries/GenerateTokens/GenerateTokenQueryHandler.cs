using Masar.Application.Common.Interfaces;
using Masar.Domain.Common.Results;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Identity.Queries.GenerateTokens;

public class GenerateTokenQueryHandler( IIdentityService identityService, ITokenProvider tokenProvider, ILogger logger)
    : IRequestHandler<GenerateTokenQuery, Result<TokenResponse>>
{

    private readonly IIdentityService _identityService = identityService;
    private readonly ITokenProvider _tokenProvider = tokenProvider;
    private readonly ILogger _logger = logger;

    public async Task<Result<TokenResponse>> Handle(GenerateTokenQuery query, CancellationToken ct)
    {
        var userResponse = await _identityService.AuthenticateAsync(query.UserName, query.Password);

        if (userResponse.IsError)
        {
            return userResponse.Errors;
        }

        var generateTokenResult = await _tokenProvider.GenerateJwtTokenAsync(userResponse.Value, ct);

        if (generateTokenResult.IsError)
        {
            
            return generateTokenResult.Errors;
        }
        _logger.LogInformation("Token generated successfully for user {UserName}", query.UserName);
        return generateTokenResult.Value;
    }
}