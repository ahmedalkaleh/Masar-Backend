using Masar.Domain.Persons;
using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class User
{
    public int UserId { get; set; }

    public int PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
