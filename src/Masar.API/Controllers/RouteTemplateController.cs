using Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate;
using Masar.Application.Features.RouteTemplates.Dtos;
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
            var result = await sender.Send(new CreateRouteTemplateCommand(request.TemplateName, request.StartStationId, request.EndStationId, RouteTemplateStops), cancellationToken);
            return result.Match(response => CreatedAtRoute(routeName: "GetRouteTemplateById", routeValues: new { id = response.RouteTemplateId }, value: response), Problem);

        }
    }
}
