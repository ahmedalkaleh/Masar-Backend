using Masar.Application.Features.Users.Commands.CreateUser;
using Masar.Application.Features.Users.Commands.UpdateUser;
using Masar.Application.Features.Users.Dtos;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/users")]
    public sealed class UsersController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new user.")]
        [EndpointDescription("Adds a new user account to the system with Identity integration.")]
        [EndpointName("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetUserById", new { id = response.UserId }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing user.")]
        [EndpointDescription("Updates user details and updates the password in Identity if provided.")]
        [EndpointName("UpdateUser")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var command = new UpdateUserCommand(
                id,
                request.PersonId,
                request.Username,
                request.NewPassword,
                request.Role,
                request.IsDelete
            );

            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }
    }
}