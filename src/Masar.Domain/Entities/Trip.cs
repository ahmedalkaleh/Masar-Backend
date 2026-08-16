using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Trip
{
    public int TripId { get; set; }

    public int TrainId { get; set; }

    public int OriginStationId { get; set; }

    public int DestinationStationId { get; set; }

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
}
