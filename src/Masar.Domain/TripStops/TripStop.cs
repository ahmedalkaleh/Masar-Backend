using Masar.Domain.Common;
using Masar.Domain.Stations;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.TripStops;

public partial class TripStop : AuditableEntity
{
    public Guid TripId { get; set; }

    public Guid StationId { get; set; }

    public int StopOrder { get; set; }

    public DateTime ScheduledArrival { get; set; }

    public DateTime ScheduledDeparture { get; set; }

    public int DwellTimeMinutes { get; set; }

    public bool IsCustomsCheck { get; set; }

    public bool IsDelete { get; set; }

    public virtual Station Station { get; set; } = null!;

    public virtual Trip Trip { get; set; } = null!;


    private TripStop() { }

    private TripStop(
    Guid id,
    Guid tripId,
    Guid stationId,
    int stopOrder,
    DateTime scheduledArrival,
    DateTime scheduledDeparture,
    int dwellTimeMinutes,
    bool isCustomsCheck,
    bool isDelete)
        :base(id)
    {
        TripId = tripId;
        StationId = stationId;
        StopOrder = stopOrder;
        ScheduledArrival = scheduledArrival;
        ScheduledDeparture = scheduledDeparture;
        DwellTimeMinutes = dwellTimeMinutes;
        IsCustomsCheck = isCustomsCheck;
        IsDelete = isDelete;
    }
}
