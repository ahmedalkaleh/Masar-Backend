using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using Masar.Domain.Stations;
using Masar.Domain.TrainLiveLocations;
using System;
using System.Collections.Generic;

namespace Masar.Domain.RouteSegments;

public partial class RouteSegment : AuditableEntity
{
    public Guid FromStationId { get; private set; }

    public Guid ToStationId { get; private set; }

    public TrackType TrackType { get; private set; }

    public decimal DistanceKm { get; private set; }

    public int EstPassengerTimeMin { get; private set; }

    public string CorridorName { get; private set; } = null!;

    public bool IsDelete { get; private set; }

    public virtual Station FromStation { get; private set; } = null!;

    public virtual Station ToStation { get; private set; } = null!;

    public virtual ICollection<TrainLiveLocation> TrainLiveLocations { get; private set; } = new List<TrainLiveLocation>();

    private RouteSegment() { }

    private RouteSegment(
    Guid id,
    Guid fromStationId,
    Guid toStationId,
    TrackType trackType,
    decimal distanceKm,
    int estPassengerTimeMin,
    string corridorName)
        :base(id)
    {
        FromStationId = fromStationId;
        ToStationId = toStationId;
        TrackType = trackType;
        DistanceKm = distanceKm;
        EstPassengerTimeMin = estPassengerTimeMin;
        CorridorName = corridorName;


        IsDelete = false;
    }


    public static Result<RouteSegment> Create(
    Guid id,
    Guid fromStationId,
    Guid toStationId,
    TrackType trackType,
    decimal distanceKm,
    int estPassengerTimeMin,
    string corridorName)
    {
        var errorsList = new List<Error>();

        if (fromStationId == Guid.Empty)
        {
            errorsList.Add(RouteSegmentErrors.FromStationIdRequired);
        }

        if (toStationId == Guid.Empty)
        {
            errorsList.Add(RouteSegmentErrors.ToStationIdRequired);
        }

        if (!Enum.IsDefined(typeof(TrackType), trackType))
        {
            errorsList.Add(RouteSegmentErrors.InvalidTrackType);
        }

        if (distanceKm < 0m || distanceKm > 9999.99m)
        {
            errorsList.Add(RouteSegmentErrors.InvalidDistanceKm);
        }

        if (estPassengerTimeMin < 0)
        {
            errorsList.Add(RouteSegmentErrors.InvalidEstPassengerTimeMin);
        }

        if (string.IsNullOrEmpty(corridorName))
        {
            errorsList.Add(RouteSegmentErrors.CorridorNameRequired);
        }

        if(corridorName.Length > 100)
        {
            errorsList.Add(RouteSegmentErrors.CorridorNameTooLong);
        }     

        if (errorsList.Count > 0)
        {
            return errorsList;
        }


        return new RouteSegment(id, fromStationId, toStationId, trackType, distanceKm,estPassengerTimeMin,corridorName);
    }

    public Result<Updated> Update(Guid fromStationId,Guid toStationId,TrackType trackType,
         decimal distanceKm,int estPassengerTimeMin,string corridorName)
    {
        var errorsList = new List<Error>();

        if (fromStationId == Guid.Empty)
        {
            errorsList.Add(RouteSegmentErrors.FromStationIdRequired);
        }

        if (toStationId == Guid.Empty)
        {
            errorsList.Add(RouteSegmentErrors.ToStationIdRequired);
        }

        if (!Enum.IsDefined(typeof(TrackType), trackType))
        {
            errorsList.Add(RouteSegmentErrors.InvalidTrackType);
        }

        if (distanceKm < 0m || distanceKm > 9999.99m)
        {
            errorsList.Add(RouteSegmentErrors.InvalidDistanceKm);
        }

        if (estPassengerTimeMin < 0)
        {
            errorsList.Add(RouteSegmentErrors.InvalidEstPassengerTimeMin);
        }

        if (string.IsNullOrEmpty(corridorName))
        {
            errorsList.Add(RouteSegmentErrors.CorridorNameRequired);
        }

        if (corridorName.Length > 100)
        {
            errorsList.Add(RouteSegmentErrors.CorridorNameTooLong);
        }

        if (errorsList.Count > 0)
        {
            return errorsList;
        }

        FromStationId = fromStationId;
        ToStationId = toStationId;
        TrackType = trackType;
        DistanceKm = distanceKm;
        EstPassengerTimeMin = estPassengerTimeMin;
        CorridorName = corridorName;

        return Result.Updated;
    }

}
