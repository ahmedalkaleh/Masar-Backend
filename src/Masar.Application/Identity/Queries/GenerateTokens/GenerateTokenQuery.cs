using Masar.Domain.Common.Results;

using MediatR;

namespace Masar.Application.Features.Identity.Queries.GenerateTokens;

public record GenerateTokenQuery(
    string UserName,
    string Password) : IRequest<Result<TokenResponse>>;