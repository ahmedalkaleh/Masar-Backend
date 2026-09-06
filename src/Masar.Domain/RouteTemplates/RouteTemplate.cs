using Masar.Domain.Common.Results;
using Masar.Domain.Common;

using Masar.Domain.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.RouteTemplates
{
    public class RouteTemplate:AuditableEntity
    {
        public string TemplateName { get; private set; } = null!;
        public Guid StartStationId { get; private set; }
        public Guid EndStationId { get; private set; }

        public Station StartStation { get; private set; } = null!;

        public Station EndStation { get; private set; } = null!;
        
        public RouteTemplate() { }

        private RouteTemplate(
            Guid id,
            string templateName,
            Guid startStationId,
            Guid endStationId
            )
            : base(id)
        {
            TemplateName = templateName;
            StartStationId = startStationId;
            EndStationId = endStationId;
        }

        public static Result<RouteTemplate> Create(
            string templateName,
            Guid startStationId,
            Guid endStationId
            )
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return RoutTemplateErrors.TemplateNameRequired;
            }
            if(startStationId == Guid.Empty)
            {
                return RoutTemplateErrors.StartStationRequired;
            }
            if(endStationId == Guid.Empty)
            {
                return RoutTemplateErrors.EndStationRequired;
            }
            if(startStationId == endStationId)
            {
                return RoutTemplateErrors.EndStationEqualStartStation;
            }

            return new RouteTemplate(
                Guid.NewGuid(),
                templateName,
                startStationId,
                endStationId
                );
        }

        public Result<Updated> Update(
            
            string templateName,
            Guid startStationId,
            Guid endStationId
            )
        {
            if (string.IsNullOrEmpty(templateName))
            {
                return RoutTemplateErrors.TemplateNameRequired;
            }
            if (startStationId == Guid.Empty)
            {
                return RoutTemplateErrors.StartStationRequired;
            }
            if (endStationId == Guid.Empty)
            {
                return RoutTemplateErrors.EndStationRequired;
            }
            if (startStationId == endStationId)
            {
                return RoutTemplateErrors.EndStationEqualStartStation;
            }
            TemplateName = templateName;
            StartStationId = startStationId;
            EndStationId = endStationId;
            return Result.Updated;
        }
    }
}
