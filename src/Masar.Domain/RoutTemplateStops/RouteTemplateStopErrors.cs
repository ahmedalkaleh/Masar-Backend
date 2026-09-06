using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.RoutTemplateStops
{
    public static class RouteTemplateStopErrors
    {
        public static Error RoutTemplateIdRequired => Error.Validation("RoutTemplateStop.RoutTemplateIdRequired", "Rout Template Id is Required");
        public static Error StationIdRequired => Error.Validation("RoutTemplateStop.StationIdRequired", "Station Id is Required");

        public static Error StopOrderMustBeGreaterThanZero => Error.Validation("RoutTemplateStop.StopOrderMustBeGreaterThanZero", "Stop Order Must Be Greater Than Zero");
    }
}
