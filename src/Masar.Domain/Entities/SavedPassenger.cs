using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class SavedPassenger
{
    public int SavedPassengerId { get; set; }

    public int UserId { get; set; }

    public string Fullname { get; set; } = null!;

    public string NationalId { get; set; } = null!;
}
