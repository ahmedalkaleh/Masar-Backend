using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Bookings
{
    public static class BookingErrors
    {
        public static Error BookingReferenceRequired =>
            Error.Validation(
                "Booking.BookingReferenceRequired",
                "Booking reference is required.");

        public static Error BookingReferenceAlreadyExists =>
            Error.Conflict(
                "Booking.BookingReferenceAlreadyExists",
                "A booking with this reference already exists.");


        public static Error PassengerIdRequired =>
            Error.Validation(
                "Booking.PassengerIdRequired",
                "Passenger ID is required.");

        public static Error PassengerNotFound =>
            Error.NotFound(
                "Booking.PassengerNotFound",
                "Passenger with the specified ID was not found.");

        public static Error TripIdRequired =>
            Error.Validation(
                "Booking.TripIdRequired",
                "Trip ID is required.");

        public static Error TripNotFound =>
            Error.NotFound(
                "Booking.TripNotFound",
                "Trip with the specified ID was not found.");

        public static Error BoardingStationIdRequired =>
            Error.Validation(
                "Booking.BoardingStationIdRequired",
                "Boarding station ID is required.");

        public static Error BoardingStationNotFound =>
            Error.NotFound(
                "Booking.BoardingStationNotFound",
                "Boarding station with the specified ID was not found.");

        public static Error AlightingStationIdRequired =>
            Error.Validation(
                "Booking.AlightingStationIdRequired",
                "Alighting station ID is required.");

        public static Error AlightingStationNotFound =>
            Error.NotFound(
                "Booking.AlightingStationNotFound",
                "Alighting station with the specified ID was not found.");

        public static Error SameBoardingAndAlightingStation =>
            Error.Validation(
                "Booking.SameBoardingAndAlightingStation",
                "Boarding station and alighting station cannot be the same.");

        public static Error TotalPriceMustBePositive =>
            Error.Validation(
                "Booking.TotalPriceMustBePositive",
                "Total price must be greater than zero.");

        public static Error TotalPriceExceedsLimit =>
            Error.Validation(
                "Booking.TotalPriceExceedsLimit",
                "Total price cannot exceed 999,999,999,999,999.99.");

        public static Error TotalPriceTooManyDecimalPlaces =>
            Error.Validation(
                "Booking.TotalPriceTooManyDecimalPlaces",
                "Total price cannot have more than 2 decimal places.");

        public static Error InvalidPaymentStatus =>
            Error.Validation(
                "Booking.InvalidPaymentStatus",
                "The specified payment status is invalid.");

        public static Error PaidAtInFuture =>
            Error.Validation(
                "Booking.PaidAtInFuture",
                "Payment timestamp cannot be set in the future.");


        public static Error ExpiresAtInPast =>
            Error.Validation(
                "Booking.ExpiresAtInPast",
                "Expiration timestamp must be in the future.");

        public static Error BookingExpired =>
            Error.Validation(
                "Booking.BookingExpired",
                "The reservation period for this booking has expired.");

        public static Error BookingNotFound =>
            Error.NotFound(
                "Booking.NotFound",
                "Booking with the specified ID was not found.");

        public static Error CannotUpdateNonPendingBooking =>
           Error.Conflict(
                "Booking.CannotUpdateNonPendingBooking",
                "Booking can only be updated when its status is Pending.");

        public static Error TicketsListRequired =>
           Error.Validation(
                "Booking.TicketsListRequired",
                "Tickets list cannot be null.");

        public static Error AtLeastOneTicketRequired =>
           Error.Validation(
                "Booking.AtLeastOneTicketRequired",
                "Booking must contain at least one ticket.");


        public static Error BoardingStationNotInTrip =>
            Error.Validation(
                "Booking.BoardingStationNotInTrip",
                "The selected boarding station is not part of the specified trip.");

        public static Error AlightingStationIdNotInTrip =>
            Error.Validation(
                "Booking.AlightingStationIdNotInTrip",
                "The selected alighting station is not part of the specified trip.");

    }

}
