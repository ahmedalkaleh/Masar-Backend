using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Dtos
{
    public sealed record TripStopDto(Guid TripStopId, Guid StationId, int StopOrder, DateTime ScheduledArrival, DateTime ScheduledDeparture, int DwellTimeMinutes);
    
}
