using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Seats
{
    public static class SeatErrors
    {
        public static Error CarriageIdRequired =>
            Error.Validation(
                "Seat.CarriageIdRequired",
                "Carriage ID is required.");

        public static Error RowNumberRequired =>
            Error.Validation(
                "Seat.RowNumberRequired",
                "Row number is required.");

        public static Error RowNumberTooLong =>
            Error.Validation(
                "Seat.RowNumberTooLong",
                "Row number must not exceed 2 characters.");

        public static Error InvalidColumnNumber =>
            Error.Validation(
                "Seat.InvalidColumnNumber",
                "Column number must be between 1 and 6.");

        public static Error InvalidSeatType =>
            Error.Validation("Seat.InvalidSeatType", "Seat type is invalid.");

        public static Error SeatNotFound =>
            Error.NotFound(
                "Seat.NotFound",
                "Seat with the specified ID was not found.");

        public static Error CarriageNotFound =>
            Error.NotFound(
                "Carriage.NotFound",
                "Carriage with the specified ID was not found.");

        public static Error SeatPositionAlreadyExists =>
            Error.Conflict(
                "Seat.PositionAlreadyExists",
                "A seat with the specified carriage, row, and column already exists.");
    }
}
