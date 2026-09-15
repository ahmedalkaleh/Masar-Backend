using Masar.Application.Features.Trips.Commands.CreateTrip;
using Masar.Application.Features.Trips.Dtos;
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
            return result.Match(Response =>CreatedAtRoute("GetTripById", new { id = Response.TripId }, Response),Problem);
        }
    }
}
