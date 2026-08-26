using Masar.Application.Features.Identity.Dtos;
using Masar.Domain.Common.Results;

using MediatR;

namespace Masar.Application.Features.Identity.Queries.GetUserInfo;

public sealed record GetUserByIdQuery(string? UserId) : IRequest<Result<AppUserDto>>;