using Masar.Application.Features.Trains.Commands.CreateTrain;
using Masar.Application.Features.Trains.Commands.UpdateTrain;
using Masar.Application.Features.Trains.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/trains")]
    public class TrainController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(TrainDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Train.")]
        [EndpointDescription("Adds a new Train to the system.")]
        [EndpointName("CreateTrain")]
        public async Task<IActionResult> CreateTrain([FromBody] CreateTrainCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetTrainById", new { id = response.TrainID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(TrainDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing Train.")]
        [EndpointDescription("Updates the details of an existing Train in the system.")]
        [EndpointName("UpdateTrain")]
        public async Task<IActionResult> UpdateTrain(Guid id, [FromBody] UpdateTrainCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateTrainCommand(id,request.Code,request.Name,
                request.TrainType,request.MaxSpeedKmh);

            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}

