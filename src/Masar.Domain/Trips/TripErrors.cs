using System;
using Masar.Domain.Common.Results;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Trips
{
    public static class TripErrors
    {
        public static Error TrainIdRequired =>
            Error.Validation(
            "Trip.TrainIdRequired",
            "TrainId is required.");

        public static Error OriginStationIdRequired =>
            Error.Validation(
                "Trip.OriginStationIdRequired",
                "OriginStationID is required.");

        public static Error DestinationStationIdRequired =>
            Error.Validation(
                "Trip.DestinationStationIdRequired",
                "DestinationStationID is required.");

        public static Error TrainNotFound =>
            Error.NotFound(
                "Trip.TrainNotFound",
                "Train with the specified ID was not found.");

        public static Error OriginStationNotFound =>
            Error.NotFound(
                "Trip.OriginStationNotFound",
                "Origin station with the specified ID was not found.");

        public static Error DestinationStationNotFound =>
            Error.NotFound(
                "Trip.DestinationStationNotFound",
                "Destination station with the specified ID was not found.");

        public static Error SameOriginAndDestination =>
            Error.Validation(
                "Trip.SameOriginAndDestination",
                "Origin station and destination station cannot be the same.");

        public static Error DepartureTimeInPast =>
            Error.Validation(
                "Trip.DepartureTimeInPast",
                "Departure time cannot be in the past.");

        public static Error EstimatedArrivalBeforeDeparture =>
            Error.Validation(
                "Trip.EstimatedArrivalBeforeDeparture",
                "Estimated arrival time must be strictly after the departure time.");

        public static Error ActualArrivalBeforeDeparture =>
            Error.Validation(
                "Trip.ActualArrivalBeforeDeparture",
                "Actual arrival time cannot be before the departure time.");

        public static Error ActualArrivalTimeInFuture =>
            Error.Validation(
                "Trip.ActualArrivalTimeInFuture",
                "Actual arrival time cannot be set in the future.");

        public static Error InvalidStatus =>
            Error.Validation(
                "Trip.InvalidStatus",
                "The specified trip status is invalid.");

        public static Error ActualArrivalTimeNotAllowedForStatus =>
            Error.Validation(
                "Trip.ActualArrivalTimeNotAllowedForStatus",
                "Actual arrival time can only be set when the trip is completed.");

        public static Error TripNotFound =>
            Error.NotFound(
                "Trip.NotFound",
                "Trip with the specified ID was not found.");

        public static Error TrainScheduleOverlap =>
            Error.Conflict(
                "Trip.TrainScheduleOverlap",
                "The selected train is already scheduled for another trip during this time frame.");
    }
}
