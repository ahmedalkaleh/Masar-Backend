using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteSegments;
using Masar.Domain.Seats;
using Masar.Domain.Stations;
using Masar.Domain.TrainLiveLocations;
using Masar.Domain.Trains;
using Masar.Domain.TripStops;
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Masar.Domain.Trips;

public partial class Trip : AuditableEntity
{

    public Guid TrainId { get; private set; }

    public Guid OriginStationId { get; private set; }

    public Guid DestinationStationId { get; private set; }

    public DateTime DepartureTime { get; private set; }

    public DateTime EstimatedArrivalTime { get; private set; }

    public DateTime? ActualArrivalTime { get; private set; }

    public TripStatus Status { get;  set; }

    public bool IsDelete { get; private set; }

    public virtual ICollection<Booking> Bookings { get; private set; } = new List<Booking>();
     
    public virtual Station DestinationStation { get; private set; } = null!;

    public virtual Station OriginStation { get; private set; } = null!;

    public virtual Train Train { get; private set; } = null!;

    public virtual ICollection<TrainLiveLocation> TrainLiveLocations { get; private set; } = new List<TrainLiveLocation>();

    public virtual ICollection<TripStop> TripStops { get; private set; } = new List<TripStop>();

    private Trip() { }


    private Trip(
    Guid id,
    Guid trainId,
    Guid originStationId,
    Guid destinationStationId,
    DateTime departureTime,
    DateTime estimatedArrivalTime)
        :base(id)
    {
        TrainId = trainId;
        OriginStationId = originStationId;
        DestinationStationId = destinationStationId;
        DepartureTime = departureTime;
        EstimatedArrivalTime = estimatedArrivalTime;
        ActualArrivalTime = null;
        Status = TripStatus.Scheduled;
        IsDelete = false;
    }


    public static Result<Trip> Create(
    Guid id,
    Guid trainId,
    Guid originStationId,
    Guid destinationStationId,
    DateTime departureTime,
    DateTime estimatedArrivalTime)
    {
        if (trainId == Guid.Empty)
        {
            return TripErrors.TrainIdRequired;
        }

        if (originStationId == Guid.Empty)
        {
            return TripErrors.OriginStationIdRequired;
        }

        if (destinationStationId == Guid.Empty)
        {
            return TripErrors.DestinationStationIdRequired;
        }

        if(originStationId == destinationStationId)
        {
            return TripErrors.SameOriginAndDestination;
        }

        if (departureTime < DateTime.UtcNow)
        {
            return TripErrors.DepartureTimeInPast;
        }

        if (estimatedArrivalTime <= departureTime)
        {
            return TripErrors.EstimatedArrivalBeforeDeparture;
        }

        return new Trip(id, trainId, originStationId, destinationStationId, departureTime, estimatedArrivalTime);
    }


    public Result<Updated> Update(Guid trainId,Guid originStationId,Guid destinationStationId,
    DateTime departureTime,DateTime estimatedArrivalTime,TripStatus status,DateTime? actualArrivalTime)
    {
        if (trainId == Guid.Empty)
        {
            return TripErrors.TrainIdRequired;
        }

        if (originStationId == Guid.Empty)
        {
            return TripErrors.OriginStationIdRequired;
        }

        if (destinationStationId == Guid.Empty)
        {
            return TripErrors.DestinationStationIdRequired;
        }

        if (originStationId == destinationStationId)
        {
            return TripErrors.SameOriginAndDestination;
        }

        if (departureTime < DateTime.UtcNow)
        {
            return TripErrors.DepartureTimeInPast;
        }

        if (estimatedArrivalTime <= departureTime)
        {
            return TripErrors.EstimatedArrivalBeforeDeparture;
        }

        if (!Enum.IsDefined(typeof(TripStatus), status))
        {
            return TripErrors.InvalidStatus;
        }

        if(actualArrivalTime <= departureTime)
        {
            return TripErrors.ActualArrivalBeforeDeparture;
        }

        if(actualArrivalTime > DateTime.UtcNow)
        {
            return TripErrors.ActualArrivalTimeInFuture;
        }

        if(actualArrivalTime != null && status != TripStatus.Completed)
        {
            return TripErrors.ActualArrivalTimeNotAllowedForStatus;
        }
        

        TrainId = trainId;
        OriginStationId = originStationId;
        DestinationStationId = destinationStationId;
        DepartureTime = departureTime;
        EstimatedArrivalTime = estimatedArrivalTime;
        Status = status;
        ActualArrivalTime = actualArrivalTime;

        return Result.Updated;
    }




}
