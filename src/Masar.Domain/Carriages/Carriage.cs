using Masar.Domain.Common;
using Masar.Domain.Seats;
using Masar.Domain.Trains;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Carriages;

public partial class Carriage : AuditableEntity
{
    public Guid TrainId { get; set; }

    public int CarriageNumber { get; set; }

    public string ClassType { get; set; } = null!;

    public int TotalSeats { get; set; }

    public bool IsDelete { get; set; }

    public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();

    public virtual Train Train { get; set; } = null!;

    private Carriage() { }


    private Carriage(
    Guid id,
    Guid trainId,
    int carriageNumber,
    string classType,
    int totalSeats,
    bool isDelete)
        :base(id)
    {
        TrainId = trainId;
        CarriageNumber = carriageNumber;
        ClassType = classType;
        TotalSeats = totalSeats;
        IsDelete = isDelete;
    }
}
