using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;
using Masar.Domain.Identity;
using Masar.Domain.SavedPassengers;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Users;

public partial class User : AuditableEntity
{

    public Guid PersonId { get; set; }

    public string Username { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } 

    public virtual ICollection<SavedPassenger> SavedPassengers { get; set; } = new List<SavedPassenger>();


    private User() { }

    private User(
    Guid id,
    Guid personId,
    string username,
    Role role,
    bool isDelete)
        :base(id)
    {
        PersonId = personId;
        Username = username;
        Role = role;
        IsDelete = isDelete;
    }
    public static Result<User> Create(Guid id, Guid personId, string username, Role role, bool isDelete)
    {
        if (id == Guid.Empty)
        {
            return UserError.UserNotFound;
        }
        if (personId == Guid.Empty)
        {
            return UserError.PersonIdRequired;
        }
        if (string.IsNullOrWhiteSpace(username))
        {
            return UserError.UsernameRequired;
        }
        return new User(id, personId, username, role, isDelete);
    }
    public  Result<Updated> Update(Guid personId, string username, Role role, bool isDelete)
    {
        if (personId == Guid.Empty)
        {
            return UserError.PersonIdRequired;
        }
        if (string.IsNullOrWhiteSpace(username))
        {
            return UserError.UsernameRequired;
        }
        return Result.Updated;
    }
}
