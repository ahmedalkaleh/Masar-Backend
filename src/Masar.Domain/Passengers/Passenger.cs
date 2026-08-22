using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Persons;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Passengers;

public partial class Passenger : AuditableEntity
{
    public Guid PersonId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Person Person { get; set; } = null!;

    private Passenger() { }


    private Passenger(
    Guid id,
    Guid personId)
        :base(id)
    {
        PersonId = personId;
    }
}
