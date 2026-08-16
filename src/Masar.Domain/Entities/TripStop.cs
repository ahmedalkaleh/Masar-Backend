using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class TripStop
{
    public int TripStopId { get; set; }

    public int TripId { get; set; }

    public int StationId { get; set; }

    public int StopOrder { get; set; }

    public DateTime ScheduledArrival { get; set; }

    public DateTime ScheduledDeparture { get; set; }

    public int DwellTimeMinutes { get; set; }

    public bool IsCustomsCheck { get; set; }

    public bool IsDelete { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;
}
