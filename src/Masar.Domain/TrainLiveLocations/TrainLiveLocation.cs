using Masar.Domain.Common;
using Masar.Domain.RouteSegments;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.TrainLiveLocations;

public partial class TrainLiveLocation : AuditableEntity
{
    public Guid TripId { get; set; }

    public Guid? CurrentSegmentId { get; set; }

    public decimal CurrentLatitude { get; set; }

    public decimal CurrentLongitude { get; set; }

    public decimal CurrentSpeedKmh { get; set; }

    public int DelayMinutes { get; set; }

    public DateTime LastUpdatedUtcdatetime2 { get; set; }

    public bool IsDelete { get; set; }

    public virtual RouteSegment? CurrentSegment { get; set; }

    public virtual Trip Trip { get; set; } = null!;

    private TrainLiveLocation() { }


    private TrainLiveLocation(
    Guid id,
    Guid tripId,
    Guid? currentSegmentId,
    decimal currentLatitude,
    decimal currentLongitude,
    decimal currentSpeedKmh,
    int delayMinutes,
    DateTime lastUpdatedUtcdatetime2,
    bool isDelete)
        :base(id)
    {
        TripId = tripId;
        CurrentSegmentId = currentSegmentId;
        CurrentLatitude = currentLatitude;
        CurrentLongitude = currentLongitude;
        CurrentSpeedKmh = currentSpeedKmh;
        DelayMinutes = delayMinutes;
        LastUpdatedUtcdatetime2 = lastUpdatedUtcdatetime2;
        IsDelete = isDelete;
    }
}
