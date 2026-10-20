using Masar.Application.Features.RouteSegments.Commands.CreateRouteSegment;
using Masar.Application.Features.RouteSegments.Commands.UpdateRouteSegment;
using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Application.Features.RouteSegments.Queries.GetAll;
using Masar.Application.Features.RouteSegments.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/routeSegments")]
    [ApiController]
    public class RouteSegmentsController(ISender sender) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(RouteSegmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new RouteSegment.")]
        [EndpointDescription("Adds a new RouteSegment to the system.")]
        [EndpointName("CreateRouteSegment")]
        public async Task<IActionResult> CreateRouteSegment([FromBody] CreateRouteSegmentCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => StatusCode(StatusCodes.Status201Created, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(RouteSegmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing RouteSegment.")]
        [EndpointDescription("Updates the details of an existing RouteSegment in the system.")]
        [EndpointName("UpdateRouteSegment")]
        public async Task<IActionResult> UpdateRouteSegment(Guid id, [FromBody] UpdateRouteSegmentCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateRouteSegmentCommand{
             RouteSegmentID = id,
             FirstStationId = request.FirstStationId, SecondStationId = request.SecondStationId, TrackType = request.TrackType, DistanceKm = request.DistanceKm,
             EstPassengerTimeMin = request.EstPassengerTimeMin, CorridorName = request.CorridorName };


            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RouteSegmentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive route segment by Id.")]
        [EndpointDescription("Fetches detailed information for a specific route segment using its unique identifier.")]
        [EndpointName("GetRouteSegmentById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRouteSegmentByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet()]
        [ProducesResponseType(typeof(RouteSegmentDetailsDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all route segments")]
        [EndpointDescription("Retrivea list of all route segments registered in the system.")]
        [EndpointName("GetAllRouteSegments")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllRouteSegmentsQuery(); 
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}
