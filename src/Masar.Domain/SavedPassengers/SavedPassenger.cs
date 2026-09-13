using Masar.Domain.Common;
using Masar.Domain.Passengers;
using System;
using System.Collections.Generic;

namespace Masar.Domain.SavedPassengers;

public partial class SavedPassenger : AuditableEntity
{
    public Guid PassengerId { get; set; }

    public string Fullname { get; set; } = null!;

    public string NationalId { get; set; } = null!;

    public Passenger Passenger { get; set; }

    private SavedPassenger() { }


    private SavedPassenger(
    Guid id,
    Guid userId,
    string fullname,
    string nationalId)
        :base(id)
    {
        PassengerId = userId;
        Fullname = fullname;
        NationalId = nationalId;
    }
}
