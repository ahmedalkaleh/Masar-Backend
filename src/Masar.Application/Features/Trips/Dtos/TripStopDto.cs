using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trips.Dtos
{
    public class TripStopDto
    { 
        public Guid TripStopId { get; set; }
        public Guid StationId { get; set; }
        public int StopOrder { get; set; }
        public DateTime ScheduledDeparture { get; set; }
        public DateTime ScheduledArrival { get; set; }
        public int DwellTimeMinutes { get; set; }
    }
}
