using Masar.Application.Features.Carriages.Commands.CreateCarriage;
using Masar.Application.Features.Carriages.Commands.UpdateCarriage;
using Masar.Application.Features.Carriages.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/Carriages")]
    public class CarriageController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(CarriageDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Carriage.")]
        [EndpointDescription("Adds a new Carriage to the system.")]
        [EndpointName("CreateCarriage")]
        public async Task<IActionResult> CreateCarriage([FromBody] CreateCarriageCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetCarriageById", new { id = response.CarriageId }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(CarriageDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing Carriage.")]
        [EndpointDescription("Updates the details of an existing Carriage in the system.")]
        [EndpointName("UpdateCarriage")]
        public async Task<IActionResult> UpdateCarriage(Guid id, [FromBody] UpdateCarriageCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateCarriageCommand(id, request.TrainId, request.CarriageNumber,
                request.ClassType, request.TotalSeats);

            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}
