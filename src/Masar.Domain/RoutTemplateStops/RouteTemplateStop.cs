using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using Masar.Domain.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.RouteTemplateStops
{
    public sealed class RouteTemplateStop : AuditableEntity
    {
        public Guid RouteTemplateId { get; private set; }
        public Guid StationId { get; private set; }

        public int StopOrder { get; private set; }

        public RouteTemplate RouteTemplate { get; private set; } = null!;

        public Station Station { get; private set; } = null!;

        private RouteTemplateStop() { }
        private RouteTemplateStop(Guid id, Guid stationId, int stopOrder) : base(id)
        {
          
            StationId = stationId;
            StopOrder = stopOrder;
        }

        public static Result<RouteTemplateStop> Create(Guid id, Guid stationId, int stopOrder)
        {
           
            if (stationId == Guid.Empty)
            {
                return RouteTemplateStopErrors.StationIdRequired;
            }
            if (stopOrder <= 0)
            {
                return RouteTemplateStopErrors.StopOrderMustBeGreaterThanZero;
            }
            return new RouteTemplateStop(id, stationId, stopOrder);
        }
        public Result<Updated> Update(Guid routTemplateId, Guid stationId, int stopOrder)
        {
            if (routTemplateId == Guid.Empty)
            {
                return RouteTemplateStopErrors.RoutTemplateIdRequired;
            }
            if (stationId == Guid.Empty)
            {
                return RouteTemplateStopErrors.StationIdRequired;
            }
            if (stopOrder <= 0)
            {
                return RouteTemplateStopErrors.StopOrderMustBeGreaterThanZero;
            }
            RouteTemplateId = routTemplateId;
            StationId = stationId;
            StopOrder = stopOrder;
            return Result.Updated;
        }
    }
    }
