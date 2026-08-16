using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Train
{
    public int TrainId { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TrainType { get; set; } = null!;

    public int MaxSpeedKmh { get; set; }

    public string Status { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Carriage> Carriages { get; set; } = new List<Carriage>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();
}
