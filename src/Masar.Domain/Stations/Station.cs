using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.RouteSegments;
using Masar.Domain.Trips;
using Masar.Domain.TripStops;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Stations;

public partial class Station : AuditableEntity
{
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

    private Station() { }


    private Station(
    Guid id,
    string nameAr,
    string nameEn,
    string type,
    decimal latitude,
    decimal longitude,
    string governorate,
    bool hasPassingLoop,
    int customsDelayMinutes,
    DateTime createdAt,
    bool isDelete)
        :base(id)
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Type = type;
        Latitude = latitude;
        Longitude = longitude;
        Governorate = governorate;
        HasPassingLoop = hasPassingLoop;
        CustomsDelayMinutes = customsDelayMinutes;
        CreatedAt = createdAt;
        IsDelete = isDelete;
    }
}
