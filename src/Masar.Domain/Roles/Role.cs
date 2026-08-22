using Masar.Domain.Common;
using Masar.Domain.Users;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Roles;

public partial class Role : AuditableEntity
{
    public string Role1 { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();

    private Role() { }


    private Role(
    Guid id,
    string role1,
    string? description)
        :base(id)
    {
        Role1 = role1;
        Description = description;
    }
}
