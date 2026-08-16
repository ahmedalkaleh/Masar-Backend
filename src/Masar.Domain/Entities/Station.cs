using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Station
{
    public int StationId { get; set; }

    public string NameAr { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string Type { get; set; } = null!;

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string Governorate { get; set; } = null!;

    public bool HasPassingLoop { get; set; }

    public int CustomsDelayMinutes { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Booking> BookingAlightingStations { get; set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingBoardingStations { get; set; } = new List<Booking>();

    public virtual ICollection<RouteSegment> RouteSegmentFromStations { get; set; } = new List<RouteSegment>();

    public virtual ICollection<RouteSegment> RouteSegmentToStations { get; set; } = new List<RouteSegment>();

    public virtual ICollection<Trip> TripDestinationStations { get; set; } = new List<Trip>();

    public virtual ICollection<Trip> TripOriginStations { get; set; } = new List<Trip>();

    public virtual ICollection<TripStop> TripStops { get; set; } = new List<TripStop>();
}
