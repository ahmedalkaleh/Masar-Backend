using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Persons;
using Masar.Domain.Roles;
using Masar.Domain.SavedPassengers;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Users;

public partial class User : AuditableEntity
{

    public Guid PersonId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public Guid RoleId { get; set; }

    public bool IsDelete { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<SavedPassenger> SavedPassengers { get; set; } = new List<SavedPassenger>();


    private User() { }

    private User(
    Guid id,
    Guid personId,
    string username,
    string passwordHash,
    Guid roleId,
    DateTime createdAt,
    bool isDelete)
        :base(id)
    {
        PersonId = personId;
        Username = username;
        PasswordHash = passwordHash;
        RoleId = roleId;
        CreatedAt = createdAt;
        IsDelete = isDelete;
    }

}
