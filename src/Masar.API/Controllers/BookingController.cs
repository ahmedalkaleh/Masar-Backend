using Masar.Application.Features.Bookings.Commands.CreateBooking;
using Masar.Application.Features.Bookings.Dtos;
using Masar.Domain.Bookings;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/Bookings")]
    [ApiController]
    public class BookingController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Booking.")]
        [EndpointDescription("Adds a new Booking to the system.")]
        [EndpointName("CreateBooking")]
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingCommand request, CancellationToken cancellationToken)
        {

            var result = await sender.Send(request, cancellationToken); 
            return result.Match(Response => CreatedAtRoute("GetBookingById", new { id = Response.BookingID }, Response), Problem);
        }
    }
}
