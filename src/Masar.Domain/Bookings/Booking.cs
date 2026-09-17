using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Passengers;
using Masar.Domain.Stations;
using Masar.Domain.Tickets;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Bookings;

public partial class Booking : AuditableEntity
{
    public string BookingReference { get; private set; } = null!;

    public Guid PassengerId { get; private set; }

    public Guid TripId { get; private set; }

    public Guid BoardingStationId { get; private set; }

    public Guid AlightingStationId { get; private set; }

    public decimal TotalPrice { get; private set; }

    public PaymentStatus PaymentStatus { get; private set; }

    public DateTime? PaidAt { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual Station AlightingStation { get; private set; } = null!;

    public virtual Station BoardingStation { get; private set; } = null!;

    public virtual Passenger Passenger { get; private set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; private set; } = new List<Ticket>();

    public virtual Trip Trip { get; private set; } = null!;

    private Booking() { }

    public Booking(
    Guid id,
    string bookingReference,
    Guid passengerId,
    Guid tripId,
    Guid boardingStationId,
    Guid alightingStationId,
    decimal totalPrice,
    DateTime expiresAt)
        : base(id)
    {
        BookingReference = bookingReference;
        PassengerId = passengerId;
        TripId = tripId;
        BoardingStationId = boardingStationId;
        AlightingStationId = alightingStationId;
        TotalPrice = totalPrice;
        ExpiresAt = expiresAt;

        IsDelete = false;
        PaymentStatus = PaymentStatus.Pending;
    }

}
