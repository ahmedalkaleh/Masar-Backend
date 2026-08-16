using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Carriage
{
    public int CarriageId { get; set; }

    public int TrainId { get; set; }

    public int CarriageNumber { get; set; }

    public string ClassType { get; set; } = null!;

    public int TotalSeats { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual Train Train { get; set; } = null!;
}
