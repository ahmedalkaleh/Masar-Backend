using Masar.Domain.Common;
using Masar.Domain.Users;
using System;
using System.Collections.Generic;

namespace Masar.Domain.SavedPassengers;

public partial class SavedPassenger : AuditableEntity
{
    public Guid UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string NationalId { get; set; } = null!;

    public User User { get; set; }

    private SavedPassenger() { }


    private SavedPassenger(
    Guid id,
    Guid userId,
    string fullname,
    string nationalId)
        :base(id)
    {
        UserId = userId;
        Fullname = fullname;
        NationalId = nationalId;
    }
}
