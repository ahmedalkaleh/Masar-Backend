using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using Masar.Domain.Trains;
using Masar.Domain.Trips;
using Masar.Domain.TripStops;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Stations;

public partial class Station : AuditableEntity
{
    public string NameAr { get; private set; } = null!;

    public string NameEn { get; private set; } = null!;

    public StationType Type { get; private set; }

    public decimal Latitude { get; private set; }

    public decimal Longitude { get; private set; }

    public string Governorate { get; private set; } = null!;

    public int CustomsDelayMinutes { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual ICollection<Booking> BookingAlightingStations { get; private set; } = new List<Booking>();

    public virtual ICollection<Booking> BookingBoardingStations { get; private set; } = new List<Booking>();

    public virtual ICollection<RouteSegment> RouteSegmentFromStations { get; private set; } = new List<RouteSegment>();

    public virtual ICollection<RouteSegment> RouteSegmentToStations { get; private set; } = new List<RouteSegment>();

    public virtual ICollection<Trip> TripDestinationStations { get; private set; } = new List<Trip>();

    public virtual ICollection<Trip> TripOriginStations { get; private set; } = new List<Trip>();

    public virtual ICollection<TripStop> TripStops { get; private set; } = new List<TripStop>();

    public virtual ICollection<Train> Trains { get; private set; } = new List<Train>();

    private Station() { }


    private Station(
    Guid id,
    string nameAr,
    string nameEn,
    StationType type,
    decimal latitude,
    decimal longitude,
    string governorate,
    int customsDelayMinutes)
        :base(id)
    {
        NameAr = nameAr;
        NameEn = nameEn;
        Type = type;
        Latitude = latitude;
        Longitude = longitude;
        Governorate = governorate;
        CustomsDelayMinutes = customsDelayMinutes;


        IsDelete = false;
    }


    public static Result<Station> Create(
    Guid id,
    string nameAr,
    string nameEn,
    StationType type,
    decimal latitude,
    decimal longitude,
    string governorate,
    int customsDelayMinutes)
    {
        var errorsList = new List<Error>();

        if (string.IsNullOrWhiteSpace(nameAr))
        {
            errorsList.Add(StationErrors.NameArRequired);
        }

        if(nameAr.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidNameAr);
        }

        if (string.IsNullOrWhiteSpace(nameEn))
        {
            errorsList.Add(StationErrors.NameEnRequired);
        }

        if (nameEn.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidNameEn);
        }

        if (latitude < -90 || latitude > 90)
        {
            errorsList.Add(StationErrors.InvalidLatitude);
        }

        if (longitude < -180 || longitude > 180)
        {
            errorsList.Add(StationErrors.InvalidLongitude);
        }

        if (string.IsNullOrWhiteSpace(governorate))
        {
            errorsList.Add(StationErrors.GovernorateRequired);
        }

        if (governorate.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidGovernorate);
        }

        if (customsDelayMinutes < 0)
        {
            errorsList.Add(StationErrors.InvalidCustomsDelayMinutes);
        }

        if(errorsList.Count > 0)
        {
            return errorsList;
        }

        return new Station(id, nameAr, nameEn, type, latitude, longitude, governorate, customsDelayMinutes);

    }

    public Result<Updated> Update(
    string nameAr,
    string nameEn,
    StationType type,
    decimal latitude,
    decimal longitude,
    string governorate,
    int customsDelayMinutes)
    {
        var errorsList = new List<Error>();

        if (string.IsNullOrWhiteSpace(nameAr))
        {
            errorsList.Add(StationErrors.NameArRequired);
        }

        if (nameAr.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidNameAr);
        }

        if (string.IsNullOrWhiteSpace(nameEn))
        {
            errorsList.Add(StationErrors.NameEnRequired);
        }

        if (nameEn.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidNameEn);
        }

        if (latitude < -90 || latitude > 90)
        {
            errorsList.Add(StationErrors.InvalidLatitude);
        }

        if (longitude < -180 || longitude > 180)
        {
            errorsList.Add(StationErrors.InvalidLongitude);
        }

        if (string.IsNullOrWhiteSpace(governorate))
        {
            errorsList.Add(StationErrors.GovernorateRequired);
        }

        if (governorate.Count() > 100)
        {
            errorsList.Add(StationErrors.InvalidGovernorate);
        }

        if (customsDelayMinutes < 0)
        {
            errorsList.Add(StationErrors.InvalidCustomsDelayMinutes);
        }

        if (errorsList.Count > 0)
        {
            return errorsList;
        }

        NameAr = nameAr;
        NameEn = nameEn;
        Type = type;
        Latitude = latitude;
        Longitude = longitude;
        Governorate = governorate;
        CustomsDelayMinutes = customsDelayMinutes;

        return Result.Updated;


    }

}
