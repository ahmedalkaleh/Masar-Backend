using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class TrainLiveLocation
{
    public long LiveLocationId { get; set; }

    public int TripId { get; set; }

    public int? CurrentSegmentId { get; set; }

    public decimal CurrentLatitude { get; set; }

    public decimal CurrentLongitude { get; set; }

    public decimal CurrentSpeedKmh { get; set; }

    public int DelayMinutes { get; set; }

    public DateTime LastUpdatedUtcdatetime2 { get; set; }

    public bool IsDelete { get; set; }

    public virtual RouteSegment? CurrentSegment { get; set; }

    public virtual Trip Trip { get; set; } = null!;
}
