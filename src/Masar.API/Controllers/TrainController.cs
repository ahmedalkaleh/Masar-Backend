using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Queries.GetAllStations;
using Masar.Application.Features.Stations.Queries.GetById;
using Masar.Application.Features.Trains.Commands.CreateTrain;
using Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;
using Masar.Application.Features.Trains.Commands.UpdateTrain;
using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Queries.GetAll;
using Masar.Application.Features.Trains.Queries.GetById;
using Masar.Application.Features.Trains.Queries.GetWithCarriagesById;
using Masar.Application.Features.Trains.Queries.GetWithSeatesById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/trains")]
    public class TrainController(ISender sender) : ApiController
    {

        [HttpPost("with-composition")]
        [ProducesResponseType(typeof(TrainDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Train with composition.")]
        [EndpointDescription("Adds a new Train to the system with all carriages and seats.")]
        [EndpointName("CreateTrainWithComposition")]
        public async Task<IActionResult> CreateTrainWithComposition(
        [FromBody] CreateTrainWithCompositionCommand command,
        CancellationToken cancellationToken)
        {
            var result = await sender.Send(command, cancellationToken);

            if (result.IsError)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }



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
            return result.Match(response => StatusCode(StatusCodes.Status201Created, response), Problem);
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

            var command = new UpdateTrainCommand{
                TrainID = id, Code = request.Code,Name = request.Name,
                TrainType = request.TrainType,MaxSpeedKmh = request.MaxSpeedKmh };

            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }



        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TrainDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive train by Id.")]
        [EndpointDescription("Fetches detailed information for a specific train using its unique identifier.")]
        [EndpointName("GetTrainById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTrainByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("{id:guid}/carriages")]
        [ProducesResponseType(typeof(TrainWithCarriagesDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive train with carriages by Id.")]
        [EndpointDescription("Fetches detailed information for a specific train along with all its attached carriages.")]
        [EndpointName("GetTrainWithCarriagesById")]
        public async Task<IActionResult> GetByIdWithCarriages(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTrainWithCarriagesByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("{id:guid}/seats")]
        [ProducesResponseType(typeof(TrainWithSeatsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive train with seats by Id.")]
        [EndpointDescription("Fetches basic train information along with its complete seat and layut.")]
        [EndpointName("GetTrainWithSeatsById")]
        public async Task<IActionResult> GetByIdWithSeats(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTrainWithSeatsByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet()]
        [ProducesResponseType(typeof(TrainSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all trains")]
        [EndpointDescription("Retrivea list of all trains registered in the system.")]
        [EndpointName("GetAllTrains")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllTrainsQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("Active")]
        [ProducesResponseType(typeof(TrainSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all active trains")]
        [EndpointDescription("Retrivea list of all active trains registered in the system.")]
        [EndpointName("GetAllActiveTrains")]
        public async Task<IActionResult> GetAllActiveTrains(CancellationToken cancellationToken)
        {
            var query = new GetAllActiveTrainsQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }




    }
}

