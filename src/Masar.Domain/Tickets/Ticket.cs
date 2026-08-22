using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Seats;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Tickets;

public partial class Ticket : AuditableEntity
{
    public Guid BookingId { get; set; }

    public Guid SeatId { get; set; }

    public string Fullname { get; set; } = null!;

    public int StartStopOrder { get; set; }

    public int EndStopOrder { get; set; }

    public decimal Price { get; set; }

    public string QrcodeHash { get; set; } = null!;

    public string Status { get; set; } = null!;

    public DateTime? BoardedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual Booking Booking { get; set; } = null!;

    public virtual Seat Seat { get; set; } = null!;

    private Ticket() { }


    private Ticket(
    Guid id,
    Guid bookingId,
    Guid seatId,
    string fullname,
    int startStopOrder,
    int endStopOrder,
    decimal price,
    string qrcodeHash,
    string status,
    DateTime? boardedAt,
    bool isDelete)
        :base(id)
    {
        BookingId = bookingId;
        SeatId = seatId;
        Fullname = fullname;
        StartStopOrder = startStopOrder;
        EndStopOrder = endStopOrder;
        Price = price;
        QrcodeHash = qrcodeHash;
        Status = status;
        BoardedAt = boardedAt;
        IsDelete = isDelete;
    }
}
