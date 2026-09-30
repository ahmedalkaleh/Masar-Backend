using Masar.Application.Features.RouteTemplates.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.RouteTemplates.Commands.CreateRouteTemplate
{
    public record CreateRouteTemplateCommand : IRequest<Result<RouteTemplateDto>>
    {
        /// <summary>
        /// The name of the route template.
        /// Maximum length: 100 characters.
        /// </summary>
        public string TemplateName { get; init; } = null!;

        /// <summary>
        /// The starting station of the route template.
        /// </summary>
        public Guid StartStationId { get; init; }

        /// <summary>
        /// The ending station of the route template.
        /// </summary>
        public Guid EndStationId { get; init; }

        /// <summary>
        /// The stops included in the route template.
        /// </summary>
        public List<CreateRouteTemplateStopCommand> RouteTemplateStops { get; init; } = [];
    }
}
