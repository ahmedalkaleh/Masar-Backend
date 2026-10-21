using Masar.Application.Features.RouteSegments.Dtos;
using Masar.Application.Features.RouteSegments.Queries.GetById;
using Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate;
using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Application.Features.RouteTemplates.Queries.GetAll;
using Masar.Application.Features.RouteTemplates.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace Masar.API.Controllers
{

    [Route("api/RouteTemplates")]
    public class RouteTemplateController(ISender sender) : ApiController
    {
        [HttpPost]
        [ProducesResponseType(typeof(RouteTemplateDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Creates a new RouteTemplate.")]
        [EndpointDescription("Adds a new RouteTemplate to the system.")]
        [EndpointName("CreateRouteTemplate")]
        public async Task<IActionResult> CreateRouteTemplate([FromBody] CreateRouteTemplateCommand request, CancellationToken cancellationToken)
        {
            var RouteTemplateStops = request.RouteTemplateStops.ConvertAll(v => new CreateRouteTemplateStopCommand(v.StationId, v.StopOrder));
            var result = await sender.Send(request, cancellationToken);
            return result.Match(response => StatusCode(StatusCodes.Status201Created, response), Problem);

        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(RouteTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrive route template by Id.")]
        [EndpointDescription("Fetches detailed information for a specific route template using its unique identifier.")]
        [EndpointName("GetRouteTemplateById")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetRouteTemplateByIdQuery(id);
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


        [HttpGet()]
        [ProducesResponseType(typeof(RouteTemplateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [EndpointSummary("Retrivea all route templates")]
        [EndpointDescription("Retrivea list of all route templates registered in the system.")]
        [EndpointName("GetAllRouteTemplates")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllRouteTemplatesQuery();
            var result = await sender.Send(query, cancellationToken);
            return result.Match(response => Ok(response), Problem);
        }


    }
}
