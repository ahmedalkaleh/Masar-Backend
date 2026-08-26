using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Carriages
{
    public static class CarriageErrors
    {

        public static Error TrainIdRequired =>
            Error.Validation(
                "Carriage.TrainIdRequired",
                "Train ID is required.");

        public static Error InvalidCarriageNumber =>
            Error.Validation(
                "Carriage.InvalidCarriageNumber",
                "Carriage number cannot be negative.");

        public static Error ClassTypeRequired =>
            Error.Validation(
                "Carriage.ClassTypeRequired",
                "Carriage class type is required.");

        public static Error InvalidTotalSeats =>
            Error.Validation(
                "Carriage.InvalidTotalSeats",
                "Total seats must be greater than 0.");

        public static Error CarriageNotFound =>
            Error.NotFound(
                "Carriage.NotFound",
                "Carriage with the specified ID was not found.");

        public static Error TrainNotFound =>
            Error.NotFound(
                "Carriage.TrainNotFound",
                "Train with the specified ID was not found.");

        public static Error CarriageNumberAlreadyExists =>
            Error.Conflict(
                "Carriage.CarriageNumberAlreadyExists",
                "A carriage with this number already exists.");

    }
}
