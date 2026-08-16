using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Seat
{
    public int SeatId { get; set; }

    public int CarriageId { get; set; }

    public string SeatNumber { get; set; } = null!;

    public int RowNumber { get; set; }

    public string ColumnPosition { get; set; } = null!;

    public string SeatType { get; set; } = null!;

    public bool IsActive { get; set; }

    public bool IsDelete { get; set; }

    public virtual Carriage Carriage { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}
