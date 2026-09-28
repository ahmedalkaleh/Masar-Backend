using Masar.Domain.Common.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Passengers
{
    public static class PassengerErrors
    {
        public static Error PassengerNotFound =>
            Error.NotFound(" Passenger.NotFound", " Passenger with the specified ID was not found.");

    }
}
