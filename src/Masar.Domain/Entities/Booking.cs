using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class Booking
{
    public int BookingId { get; set; }

    public string BookingReference { get; set; } = null!;

    public int PassengerId { get; set; }

    public int TripId { get; set; }

    public int BoardingStationId { get; set; }

    public int AlightingStationId { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsDelete { get; set; }

    public virtual Station AlightingStation { get; set; } = null!;

    public virtual Station BoardingStation { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Trip Trip { get; set; } = null!;
}
