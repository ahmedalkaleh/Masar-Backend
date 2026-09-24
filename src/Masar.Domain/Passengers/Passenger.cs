using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;
using Masar.Domain.SavedPassengers;
using Masar.Domain.Seats;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Passengers;

public partial class Passenger : AuditableEntity
{
    public Guid PersonId { get; private set; }

    public virtual ICollection<Booking> Bookings { get; private set; } = new List<Booking>();

    public virtual Person Person { get; private set; } = null!;

    public virtual ICollection<SavedPassenger> SavedPassengers { get; private set; } = new List<SavedPassenger>();
    private Passenger() { }


    private Passenger(
    Guid id,
    Person person)
        :base(id)
    {
        Person = person;
    }

    public static Result<Passenger> Create(
    Guid id,
    Person person)
    {
        return new Passenger(id, person);
    }

}
