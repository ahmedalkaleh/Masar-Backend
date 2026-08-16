using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Passenger
{
    public int PassengerId { get; set; }

    public int PersonId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Person Person { get; set; } = null!;
}
