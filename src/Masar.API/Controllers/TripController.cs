using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Queries.GetAllStations;
using Masar.Application.Features.Stations.Queries.GetById;
using Masar.Application.Features.Trips.Commands.CreateTrip;
using Masar.Application.Features.Trips.Dtos;
using Masar.Application.Features.Trips.Queries.GetAll;
using Masar.Application.Features.Trips.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace Masar.API.Controllers
{
    [Route("api/Trips")]
    public class TripController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(TripDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Trip.")]
        [EndpointDescription("Adds a new Trip to the system.")]
        [EndpointName("CreateTrip")]
        public async Task<IActionResult> CreateTrip([FromBody] CreateTripCommand request, CancellationToken cancellationToken)
        {
            
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => StatusCode(StatusCodes.Status201Created, response),Problem);
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(TripDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive trip by Id.")]
        [EndpointDescription("Fetches detailed information for a specific trip using its unique identifier.")]
        [EndpointName("GetTripById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetTripByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet()]
        [ProducesResponseType(typeof(TripSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all trips")]
        [EndpointDescription("Retrivea list of all trips registered in the system.")]
        [EndpointName("GetAllTrips")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllTripsQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }



    }
}
