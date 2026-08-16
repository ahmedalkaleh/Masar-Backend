using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Person
{
    public int PersonId { get; set; }

    public string FullName { get; set; } = null!;

    public byte[] Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public virtual ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
