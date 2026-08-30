using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Tickets;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Seats;

public partial class Seat : AuditableEntity
{
    public Guid CarriageId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public string RowNumber { get; set; } = null!;

    public byte ColumnNumber { get; set; } 

    public SeatType SeatType { get; set; }

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public virtual Carriage Carriage { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    private Seat() { }


    private Seat(
    Guid id,
    Guid carriageId,
    string rowNumber,
    byte columnNumber,
    SeatType seatType)
        :base(id)
    {
        CarriageId = carriageId;       
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        SeatType = seatType;

        SeatNumber = rowNumber.Trim() + ColumnNumber.ToString();
        IsActive = true;
        IsDelete = false;
    }
    
}
