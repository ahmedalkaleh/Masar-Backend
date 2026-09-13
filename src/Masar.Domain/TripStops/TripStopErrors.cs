using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.TripStops
{
    public static class TripStopErrors
    {
        public static Error TripIdRequired =>
            Error.Validation(
                "TripStop.TripIdRequired",
                "Trip ID is required.");

        public static Error TripNotFound =>
            Error.NotFound(
                "TripStop.TripNotFound",
                "Trip with the specified ID was not found.");

        public static Error StationIdRequired =>
            Error.Validation(
                "TripStop.StationIdRequired",
                "Station ID is required.");

        public static Error StationNotFound =>
            Error.NotFound(
                "TripStop.StationNotFound",
                "Station with the specified ID was not found.");

        public static Error InvalidStopOrder =>
            Error.Validation(
                "TripStop.InvalidStopOrder",
                "Stop order must be greater than zero.");

        public static Error DuplicateStopOrder =>
            Error.Conflict(
                "TripStop.DuplicateStopOrder",
                "A stop with the same order already exists in this trip.");

        public static Error DuplicateStationInTrip =>
            Error.Conflict(
                "TripStop.DuplicateStationInTrip",
                "This station is already added to the trip schedule.");

        public static Error InvalidScheduledArrival =>
            Error.Validation(
                "TripStop.InvalidScheduledArrival",
                "Scheduled arrival time must align with the overall trip schedule.");

        public static Error NegativeDwellTimeMinutes =>
            Error.Validation(
                "TripStop.NegativeDwellTimeMinutes",
                "Dwell time minutes cannot be negative.");

        public static Error TripStopNotFound =>
            Error.NotFound(
                "TripStop.NotFound",
                "Trip stop with the specified details was not found.");
    }
}
