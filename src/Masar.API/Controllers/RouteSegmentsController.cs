using Masar.Application.Features.RouteSegments.Commands.CreateRouteSegment;
using Masar.Application.Features.RouteSegments.Commands.UpdateRouteSegment;
using Masar.Application.Features.RouteSegments.Dtos;
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
            return result.Match(response => CreatedAtRoute("GetRouteSegmentById", new { id = response.RouteSegmentID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(RouteSegmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing RouteSegment.")]
        [EndpointDescription("Updates the details of an existing RouteSegment in the system.")]
        [EndpointName("UpdateRouteSegment")]
        public async Task<IActionResult> UpdateRouteSegment(Guid id, [FromBody] UpdateRouteSegmentCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateRouteSegmentCommand(
             id,
             request.FromStationId, request.ToStationId, request.TrackType, request.DistanceKm,
             request.EstPassengerTimeMin, request.CorridorName);


            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }
    }
}
