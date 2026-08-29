using Masar.Application.Features.Stations.Commands.CreateStation;
using Masar.Application.Features.Stations.Commands.UpdateStation;
using Masar.Application.Features.Stations.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Masar.API.Controllers
{
    [Route("api/stations")]
    public class StationsController(ISender sender) : ApiController
    {
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(typeof(StationDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new Station.")]
        [EndpointDescription("Adds a new Station to the system.")]
        [EndpointName("CreateStation")]
        public async Task<IActionResult> CreateStation([FromBody] CreateStationCommand request, CancellationToken cancellationToken)
        {
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => CreatedAtRoute("GetStationById", new { id = response.StationID }, response), Problem);
        }

        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(StationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Updates an existing Station.")]
        [EndpointDescription("Updates the details of an existing Station in the system.")]
        [EndpointName("UpdateStation")]
        public async Task<IActionResult> UpdateStation(Guid id, [FromBody] UpdateStationCommand request, CancellationToken cancellationToken)
        {

            var command = new UpdateStationCommand(
             id,
             request.NameAr,request.NameEn,request.Type,request.Latitude,
             request.Longitude,request.Governorate,request.CustomsDelayMinutes);


            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


    }
}
