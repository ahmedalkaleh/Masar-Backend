using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Tickets
{
    public static class TicketErrors
    {
        public static Error FullNameRequired =>
            Error.Validation(
                "Booking.FullNameRequired",
                "Full Name is required.");

        public static Error SeatIdRequired =>
            Error.Validation(
                "Booking.SeatId",
                "Seat ID is required.");

        public static Error SeatNotFound =>
            Error.NotFound(
                "Booking.SeatNotFound",
                "Seat with the specified ID was not found.");

        public static Error StartStopOrderMustBePositive =>
            Error.Validation(
                "Booking.StartStopOrderMustBePositive",
                "Start Stop Order must be greater than or equal zero.");

        public static Error EndStopOrderMustBePositive =>
            Error.Validation(
                "Booking.EndStopOrderMustBePositive",
                "End Stop Order must be greater than zero.");

        public static Error PriceMustBePositive =>
            Error.Validation(
                "Booking.PriceMustBePositive",
                "Total price must be greater than zero.");

        public static Error PriceExceedsLimit =>
            Error.Validation(
                "Booking.PriceExceedsLimit",
                "Total price cannot exceed 999,999,999,999,999.99.");

        public static Error PriceTooManyDecimalPlaces =>
            Error.Validation(
                "Booking.PriceTooManyDecimalPlaces",
                "Total price cannot have more than 2 decimal places.");


        public static Error TicketNotFound =>
            Error.NotFound(
                "Ticket.NotFound",
                "Ticket with the specified ID was not found.");
    }
}
