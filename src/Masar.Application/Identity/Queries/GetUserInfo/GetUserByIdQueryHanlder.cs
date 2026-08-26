using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Identity.Dtos;
using Masar.Domain.Common.Results;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Identity.Queries.GetUserInfo;

public class GetUserByIdQueryHanlder( IIdentityService identityService)
    : IRequestHandler<GetUserByIdQuery, Result<AppUserDto>>
{
   
    private readonly IIdentityService _identityService = identityService;

    public async Task<Result<AppUserDto>> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        var getUserByIdResult = await _identityService.GetUserByIdAsync(request.UserId!);

        if (getUserByIdResult.IsError)
        {
           
            return getUserByIdResult.Errors;
        }

        return getUserByIdResult.Value;
    }
}