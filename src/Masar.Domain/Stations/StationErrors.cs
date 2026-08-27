using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Stations
{
    public static class StationErrors
    {
        public static Error NameArRequired =>
            Error.Validation(
                "Station.NameArRequired",
                "Arabic station name is required.");

        public static Error InvalidNameAr =>
            Error.Validation(
                "Station.InvalidNameAr",
                "Arabic station name must not exceed 100 characters.");

        public static Error NameEnRequired =>
            Error.Validation(
                "Station.NameEnRequired",
                "English station name is required.");

        public static Error InvalidNameEn =>
            Error.Validation(
                "Station.InvalidNameEn",
                "English station name must not exceed 100 characters.");

        public static Error InvalidLatitude =>
            Error.Validation(
                "Station.InvalidLatitude",
                "Latitude must be between -90 and 90.");

        public static Error InvalidLongitude =>
            Error.Validation(
                "Station.InvalidLongitude",
                "Longitude must be between -180 and 180.");

        public static Error GovernorateRequired =>
            Error.Validation(
                "Station.GovernorateRequired",
                "Governorate is required.");

        public static Error InvalidGovernorate =>
            Error.Validation(
                "Station.InvalidGovernorate",
                "Governorate must not exceed 100 characters.");

        public static Error InvalidCustomsDelayMinutes =>
            Error.Validation(
                "Station.InvalidCustomsDelayMinutes",
                "Customs delay minutes cannot be negative.");

        public static Error StationNotFound =>
            Error.NotFound(
                "Station.NotFound",
                "Station with the specified ID was not found.");
    }
}
