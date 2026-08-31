using Masar.Application.Features.Seats.Commands.CreateSeat;
using Masar.Application.Features.Seats.Commands.UpdateSeat;
using Masar.Application.Features.Seats.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/seats")]
    public class SeatsController(ISender sender) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(SeatDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Seat.")]
        [EndpointDescription("Adds a new Seat to the system.")]
        [EndpointName("CreateSeat")]
        public async Task<IActionResult> CreateSeat([FromBody] CreateSeatCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetSeatById", new { id = response.SeatID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(SeatDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing Seat.")]
        [EndpointDescription("Updates the details of an existing Seat in the system.")]
        [EndpointName("UpdateSeat")]
        public async Task<IActionResult> UpdateSeat(Guid id, [FromBody] UpdateSeatCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateSeatCommand(
             id,
             request.RowNumber, request.ColumnNumber, request.SeatType, request.isActive);


            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}
