using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using Masar.Domain.Trains;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace Masar.Domain.TripStops;

public partial class TripStop : AuditableEntity
{
    public Guid TripId { get; private set; }

    public Guid StationId { get; private set; }

    public int StopOrder { get; private set; }

    public DateTime ScheduledArrival { get; private set; }

    public DateTime ScheduledDeparture { get; private set; }

    public int DwellTimeMinutes { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual Station Station { get; private set; } = null!;

    public virtual Trip Trip { get; private set; } = null!;


    private TripStop() { }

    private TripStop(
    Guid id,
    Guid tripId,
    Guid stationId,
    int stopOrder,
    DateTime scheduledArrival,
    int dwellTimeMinutes)
        :base(id)
    {
        TripId = tripId;
        StationId = stationId;
        StopOrder = stopOrder;
        ScheduledArrival = scheduledArrival;
        DwellTimeMinutes = dwellTimeMinutes;

        ScheduledDeparture = scheduledArrival.AddMinutes(dwellTimeMinutes);
        IsDelete = false;

    }


    public static Result<TripStop> Create(
    Guid id,
    Guid tripId,
    Guid stationId,
    int stopOrder,
    DateTime scheduledArrival,
    int dwellTimeMinutes)
    {
        if (tripId == Guid.Empty)
        {
            return TripStopErrors.TripIdRequired;
        }

        if (stationId == Guid.Empty)
        {
            return TripStopErrors.StationIdRequired;
        }

        if (stopOrder < 0)
        {
            return TripStopErrors.InvalidStopOrder;
        }

        if (dwellTimeMinutes < 0)
        {
            return TripStopErrors.NegativeDwellTimeMinutes;
        }

        return new TripStop(id, tripId, stationId, stopOrder, scheduledArrival, dwellTimeMinutes);
    }


    public Result<Updated> Update(Guid tripId, Guid stationId, int stopOrder,
        DateTime scheduledArrival, int dwellTimeMinutes)
    {
        if (tripId == Guid.Empty)
        {
            return TripStopErrors.TripIdRequired;
        }

        if (stationId == Guid.Empty)
        {
            return TripStopErrors.StationIdRequired;
        }

        if (stopOrder < 0)
        {
            return TripStopErrors.InvalidStopOrder;
        }

        if (dwellTimeMinutes < 0)
        {
            return TripStopErrors.NegativeDwellTimeMinutes;
        }


        TripId = tripId;
        StationId = stationId;
        StopOrder = stopOrder;
        ScheduledArrival = scheduledArrival;
        DwellTimeMinutes = dwellTimeMinutes;
        ScheduledDeparture = ScheduledArrival.AddMinutes(DwellTimeMinutes);

        return Result.Updated;
    }
}
