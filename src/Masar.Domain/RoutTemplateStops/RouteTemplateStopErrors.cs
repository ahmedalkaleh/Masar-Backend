using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.RouteTemplateStops
{
    public static class RouteTemplateStopErrors
    {
        public static Error RoutTemplateIdRequired => Error.Validation("RoutTemplateStop.RoutTemplateIdRequired", "Rout Template Id is Required");
        public static Error StationIdRequired => Error.Validation("RoutTemplateStop.StationIdRequired", "Station Id is Required");

        public static Error StationNotFound => Error.NotFound("RouteTemplateStop.StationNotFound", "Station with the specified ID was not found.");

        public static Error DuplicateStop => Error.Conflict("RouteTemplateStop.DuplicateStop", "Duplicate Stop Station for the same Route Template is not allowed.");
        public static Error StopOrderMustBeGreaterThanZero => Error.Validation("RoutTemplateStop.StopOrderMustBeGreaterThanZero", "Stop Order Must Be Greater Than Zero");

        public static Error InconsistentStopOrders => Error.Validation("RoutTemplateStop.InconsistentStopOrders", "Inconsistent Stop Orders: Stop orders must be unique and sequential starting from 1.");
        public static Error RouteSegmentNotFound => Error.NotFound("RouteTemplateStop.RouteSegmentNotFound", "Route segment from the specified stations was not found.");
    }
}
