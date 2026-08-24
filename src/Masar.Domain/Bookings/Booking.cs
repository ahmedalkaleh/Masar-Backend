using Masar.Domain.Common;
using Masar.Domain.Passengers;
using Masar.Domain.Stations;
using Masar.Domain.Tickets;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Bookings;

public partial class Booking : AuditableEntity
{
    public string BookingReference { get; set; } = null!;

    public Guid PassengerId { get; set; }

    public Guid TripId { get; set; }

    public Guid BoardingStationId { get; set; }

    public Guid AlightingStationId { get; set; }

    public decimal TotalPrice { get; set; }

    public string PaymentStatus { get; set; } = null!;

    public bool IsDelete { get; set; }

    public virtual Station AlightingStation { get; set; } = null!;

    public virtual Station BoardingStation { get; set; } = null!;

    public virtual Passenger Passenger { get; set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

    public virtual Trip Trip { get; set; } = null!;



    private Booking() { }

    public Booking(
    Guid id,
    string bookingReference,
    Guid passengerId,
    Guid tripId,
    Guid boardingStationId,
    Guid alightingStationId,
    decimal totalPrice,
    string paymentStatus,
    DateTime createdAt,
    bool isDelete)
        : base(id)
    {
        BookingReference = bookingReference;
        PassengerId = passengerId;
        TripId = tripId;
        BoardingStationId = boardingStationId;
        AlightingStationId = alightingStationId;
        TotalPrice = totalPrice;
        PaymentStatus = paymentStatus;
        CreatedAt = createdAt;
        IsDelete = isDelete;
    }
}
