using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Tickets;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Seats;

public partial class Seat : AuditableEntity
{
    public Guid CarriageId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public int RowNumber { get; set; }

    public string ColumnPosition { get; set; } = null!;

    public string SeatType { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public virtual Carriage Carriage { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    private Seat() { }


    private Seat(
    Guid id,
    Guid carriageId,
    string seatNumber,
    int rowNumber,
    string columnPosition,
    string seatType,
    bool isActive,
    bool isDelete)
        :base(id)
    {
        CarriageId = carriageId;
        SeatNumber = seatNumber;
        RowNumber = rowNumber;
        ColumnPosition = columnPosition;
        SeatType = seatType;
        IsActive = isActive;
        IsDelete = isDelete;
    }
}
