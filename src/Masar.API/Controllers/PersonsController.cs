
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Dtos;
using MediatR;


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
    }
}
