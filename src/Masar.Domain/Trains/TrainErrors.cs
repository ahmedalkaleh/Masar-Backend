using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Trains
{
    public static class TrainErrors
    {
        public static Error CodeRequired =>
            Error.Validation("Train.CodeRequired","Train code is required.");

        public static Error CodeAlreadyExists =>
            Error.Conflict("Train.CodeAlreadyExists", "A train with this code already exists.");

        public static Error NameRequired =>
            Error.Validation("Train.NameRequired","Train name is required.");

        public static Error TrainTypeRequired =>
            Error.Validation("Train.TrainTypeRequired","Train type is required.");

        public static Error InvalidMaxSpeedKmh =>
            Error.Validation("Train.InvalidMaxSpeedKmh","Maximum speed must be greater than 0 km/h.");

        public static Error TrainNotFound =>
            Error.NotFound("Train.NotFound", "Train with the specified ID was not found.");

        public static Error StatusRequired =>
            Error.Validation("Train.StatusRequired","Train status is required.");

    }
}
