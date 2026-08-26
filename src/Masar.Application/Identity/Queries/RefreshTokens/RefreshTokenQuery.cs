using Masar.Domain.Common.Results;

using MediatR;

namespace Masar.Application.Features.Identity.Queries.RefreshTokens;

public record RefreshTokenQuery(string RefreshToken, string ExpiredAccessToken) : IRequest<Result<TokenResponse>>;