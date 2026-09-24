using Masar.Application.Features.Passengers.Commands.CreatePassenger;
using Masar.Application.Features.Passengers.Commands.UpdatePassenger;
using Masar.Application.Features.Passengers.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController(ISender sender) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(PassengerDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Passenger.")]
        [EndpointDescription("Adds a new Passenger to the system.")]
        [EndpointName("CreatePassenger")]
        public async Task<IActionResult> CreatePassenger([FromBody] CreatePassengerCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetPassengerById", new { id = response.PassengerID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PassengerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing Passenger.")]
        [EndpointDescription("Updates the details of an existing Passenger in the system.")]
        [AllowAnonymous]
        [EndpointName("UpdatePassenger")]
        public async Task<IActionResult> UpdatePassenger(Guid id, [FromBody] UpdatePassengerCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdatePassengerCommand(
             id,
             request.Person
             );
            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }
    }
}
