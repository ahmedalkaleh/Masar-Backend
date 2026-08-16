using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Ticket
{
    public int TicketId { get; set; }

    public int BookingId { get; set; }

    public int SeatId { get; set; }

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
}
