using System;
using System.Collections.Generic;
using System.Text;

using Masar.Domain.Common.Results;
namespace Masar.Domain.RouteTemplates
{
    public static class RouteTemplateErrors
    {
        public static Error TemplateNameRequired=> Error.Validation("RouteTemplate.TemplateNameRequired", "Template name is required.");

        public static Error StartStationRequired => Error.Validation("RouteTemplate.StartStationRequired", "Start station is required.");

        public static Error EndStationRequired => Error.Validation("RouteTemplate.EndStationRequired", "End station is required.");

        public static Error StartStationNotFound => Error.NotFound("RouteTemplate.StartStationNotFound", "Start station with the specified ID was not found.");

        public static Error EndStationNotFound => Error.NotFound("RouteTemplate.EndStationNotFound", "End station with the specified ID was not found.");

        public static Error EndStationEqualStartStation => Error.Conflict("RouteTemplate.EndStationEqualStartStation","End Station must be different from Start Station");

    }
}
