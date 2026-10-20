using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Application.Features.RouteSegments.Queries.GetAll;
using Masar.Application.Features.RouteSegments.Queries.GetById;
using Masar.Application.Features.Stations.Commands.CreateStation;
using Masar.Application.Features.Stations.Commands.UpdateStation;
using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Queries.GetAllStations;
using Masar.Application.Features.Stations.Queries.GetById;
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
            return result.Match(response => StatusCode(StatusCodes.Status201Created, response), Problem);
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

            var command = new UpdateStationCommand{
                StationID = id,
                NameAr = request.NameAr,
                NameEn = request.NameEn,
                Type = request.Type,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Governorate = request.Governorate,
                CustomsDelayMinutes = request.CustomsDelayMinutes };


            var result = await sender.Send(command, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(StationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive station by Id.")]
        [EndpointDescription("Fetches detailed information for a specific station using its unique identifier.")]
        [EndpointName("GetStationById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetStationByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet()]
        [ProducesResponseType(typeof(StationSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all stations")]
        [EndpointDescription("Retrivea list of all stations registered in the system.")]
        [EndpointName("GetAllRouteSegments")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllStationsQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }

    }
}
