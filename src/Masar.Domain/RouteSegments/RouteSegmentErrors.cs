using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.RouteSegments
{
    public static class RouteSegmentErrors
    {
        public static Error FromStationIdRequired =>
            Error.Validation(
                "RouteSegment.FromStationIdRequired",
                "FromStationID is required.");

        public static Error ToStationIdRequired =>
            Error.Validation(
                "RouteSegment.ToStationIdRequired",
                "ToStationID is required.");

        public static Error CorridorNameRequired =>
            Error.Validation(
                "RouteSegment.CorridorNameRequired",
                "Corridor Name is required.");

        public static Error CorridorNameTooLong =>
            Error.Validation(
                "RouteSegment.CorridorNameTooLong",
                "Corridor Name must not exceed 100 characters.");

        public static Error InvalidTrackType =>
            Error.Validation(
                "RouteSegment.InvalidTrackType",
                "Track Type is invalid.");

        public static Error InvalidDistanceKm =>
            Error.Validation(
                "RouteSegment.InvalidDistanceKm",
                "Distance Km must be between 1 and 9999.99 km.");

        public static Error InvalidEstPassengerTimeMin =>
            Error.Validation(
                "RouteSegment.InvalidEstPassengerTimeMin",
                "EstPassengerTimeMin cannot be negative.");


        public static Error RouteSegmentNotFound =>
            Error.NotFound(
                "RouteSegment.NotFound",
                "RouteSegment with the specified ID was not found.");

        public static Error FromStationIdNotFound =>
            Error.NotFound(
                "RouteSegment.StationNotFound",
                "FromStation with the specified ID was not found.");

        public static Error ToStationIdNotFound =>
            Error.NotFound(
                "RouteSegment.StationNotFound",
                "ToStation with the specified ID was not found.");

        public static Error CorridorNameAlreadyExists =>
            Error.Conflict(
                "RouteSegment.CorridorNameAlreadyExists",
                "A Route Segment with this Corridor Name already exists.");

        public static Error SameDepartureAndArrivalStation =>
            Error.Validation(
                "RouteSegment.SameDepartureAndArrivalStation",
                "The origin station (FromStationId) and destination station (ToStationId) must be different");

        public static Error RouteSegmentAlreadyExists =>
            Error.Conflict(
                "RouteSegment.AlreadyExists",
                "A RouteSegment with the specified origin station (FromStationId) and destination station (ToStationId) already exists.");

    }
}
