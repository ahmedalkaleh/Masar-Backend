using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class RouteSegment
{
    public int SegmentId { get; set; }

    public int FromStationId { get; set; }

    public int ToStationId { get; set; }

    public string TrackType { get; set; } = null!;

    public decimal DistanceKm { get; set; }

    public int EstPassengerTimeMin { get; set; }

    public string CorridorName { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual Station FromStation { get; set; } = null!;

    public virtual Station ToStation { get; set; } = null!;

    public virtual ICollection<TrainLiveLocation> TrainLiveLocations { get; set; } = new List<TrainLiveLocation>();
}
