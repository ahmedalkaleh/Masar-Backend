using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Stations;
using Masar.Domain.TrainLiveLocations;
using Masar.Domain.Trains;
using Masar.Domain.TripStops;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Trips;

public partial class Trip : AuditableEntity
{

    public Guid TrainId { get; set; }

    public Guid OriginStationId { get; set; }

    public Guid DestinationStationId { get; set; }

    public DateTime DepartureTime { get; set; }

    public DateTime EstimatedArrivalTime { get; set; }

    public DateTime? ActualArrivalTime { get; set; }

    public string Status { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Station DestinationStation { get; set; } = null!;

    public virtual Station OriginStation { get; set; } = null!;

    public virtual Train Train { get; set; } = null!;

    public virtual ICollection<TrainLiveLocation> TrainLiveLocations { get; set; } = new List<TrainLiveLocation>();

    public virtual ICollection<TripStop> TripStops { get; set; } = new List<TripStop>();

    private Trip() { }


    private Trip(
    Guid id,
    Guid trainId,
    Guid originStationId,
    Guid destinationStationId,
    DateTime departureTime,
    DateTime estimatedArrivalTime,
    DateTime? actualArrivalTime,
    string status,
    DateTime createdAt,
    bool isDelete)
        :base(id)
    {
        TrainId = trainId;
        OriginStationId = originStationId;
        DestinationStationId = destinationStationId;
        DepartureTime = departureTime;
        EstimatedArrivalTime = estimatedArrivalTime;
        ActualArrivalTime = actualArrivalTime;
        Status = status;
        CreatedAt = createdAt;
        IsDelete = isDelete;
    }
}
