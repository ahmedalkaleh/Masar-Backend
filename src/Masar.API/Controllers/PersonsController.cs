
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Dtos;
using MediatR;
using Masar.Application.Features.Persons.Commands.UpdatePerson;

using Microsoft.AspNetCore.Mvc;






namespace Masar.API.Controllers


{
    [Route("api/persons")]
    public sealed class PersonsController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new person.")]
        [EndpointDescription("Adds a new person to the system.")]
        [EndpointName("CreatePerson")]
        public async Task<IActionResult> CreatePerson([FromBody] CreatePersonCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response=>CreatedAtRoute("GetPersonById", new { id = response.PersonID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing person.")]
        [EndpointDescription("Updates the details of an existing person in the system.")]
        [EndpointName("UpdatePerson")]
        public async Task<IActionResult> UpdatePerson( Guid id, [FromBody] UpdatePersonCommand request, CancellationToken cancellationToken)
        {
          
            var command = new UpdatePersonCommand(
    id,
    request.FullName,
    request.Email,
    request.PhoneNumber
);
            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}
