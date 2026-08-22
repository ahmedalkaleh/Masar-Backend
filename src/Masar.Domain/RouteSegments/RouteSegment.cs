using Masar.Domain.Common;
using Masar.Domain.Stations;
using Masar.Domain.TrainLiveLocations;
using System;
using System.Collections.Generic;

namespace Masar.Domain.RouteSegments;

public partial class RouteSegment : AuditableEntity
{
    public Guid FromStationId { get; set; }

    public Guid ToStationId { get; set; }

    public string TrackType { get; set; } = null!;

    public decimal DistanceKm { get; set; }

    public int EstPassengerTimeMin { get; set; }

    public string CorridorName { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual Station FromStation { get; set; } = null!;

    public virtual Station ToStation { get; set; } = null!;

    public virtual ICollection<TrainLiveLocation> TrainLiveLocations { get; set; } = new List<TrainLiveLocation>();

    private RouteSegment() { }

    private RouteSegment(
    Guid id,
    Guid fromStationId,
    Guid toStationId,
    string trackType,
    decimal distanceKm,
    int estPassengerTimeMin,
    string corridorName,
    bool isDelete)
        :base(id)
    {
        FromStationId = fromStationId;
        ToStationId = toStationId;
        TrackType = trackType;
        DistanceKm = distanceKm;
        EstPassengerTimeMin = estPassengerTimeMin;
        CorridorName = corridorName;
        IsDelete = isDelete;
    }
}
