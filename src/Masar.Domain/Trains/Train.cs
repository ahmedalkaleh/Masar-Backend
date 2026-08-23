using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Stations;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Trains;

public partial class Train : AuditableEntity
{
    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string TrainType { get; set; } = null!;

    public int MaxSpeedKmh { get; set; }

    public TrainStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public Guid? CurrentStationId { get; set; }

    public virtual ICollection<Carriage> Carriages { get; set; } = new List<Carriage>();

    public virtual ICollection<Trip> Trips { get; set; } = new List<Trip>();

    public virtual Station? Station { get; set; } = null!;

    private Train() { }


    private Train(
    Guid id,
    string code,
    string name,
    string trainType,
    int maxSpeedKmh,
    TrainStatus status,
    DateTime createdAt)
        :base(id)
    {
        Code = code;
        Name = name;
        TrainType = trainType;
        MaxSpeedKmh = maxSpeedKmh;
        Status = status;
        CreatedAt = createdAt;
    }
}
