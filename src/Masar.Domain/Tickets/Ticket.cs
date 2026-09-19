using Masar.Domain.Bookings;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Passengers;
using Masar.Domain.Seats;
using Masar.Domain.Trips;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Tickets;

public partial class Ticket : AuditableEntity
{
    public Guid BookingId { get; private set; }

    public Guid SeatId { get; private set; }

    public string Fullname { get; private set; } = null!;

    public int StartStopOrder { get; private set; }

    public int EndStopOrder { get; private set; }

    public decimal Price { get; private set; }

    public string QrcodeHash { get; private set; } = null!;

    public TicketStatus Status { get; private set; }

    public bool IsUsed { get; private set; }

    public DateTime? BoardedAt { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual Booking Booking { get; private set; } = null!;

    public virtual Seat Seat { get; private set; } = null!;

    private Ticket() { }


    private Ticket(
    Guid id,
    Guid seatId,
    string fullname,
    int startStopOrder,
    int endStopOrder,
    decimal price)
        :base(id)
    {
        SeatId = seatId;
        Fullname = fullname;
        StartStopOrder = startStopOrder;
        EndStopOrder = endStopOrder;
        Price = price;
        Status = TicketStatus.Pending;
        BoardedAt = null;
        IsDelete = false;
    }


    public static Result<Ticket> Create(
    Guid id,
    Guid seatId,
    string fullname,
    int startStopOrder,
    int endStopOrder,
    decimal price)
    {

        if (seatId == Guid.Empty)
        {
            return TicketErrors.SeatIdRequired;
        }

        if (string.IsNullOrWhiteSpace(fullname))
        {
            return TicketErrors.FullNameRequired;
        }      

        if(startStopOrder < 0)
        {
            return TicketErrors.StartStopOrderMustBePositive;
        }

        if (endStopOrder <= 0)
        {
            return TicketErrors.EndStopOrderMustBePositive;
        }

        if (price <= 0)
        {
            return TicketErrors.PriceMustBePositive;
        }

        if (decimal.Remainder(price * 100, 1) != 0)
        {
            return TicketErrors.PriceTooManyDecimalPlaces;
        }

        if (price > 9999999999999999.99m)
        {
            return TicketErrors.PriceExceedsLimit;
        }


        return new Ticket(id, seatId, fullname, startStopOrder, endStopOrder, price);

    }

    public Result<Updated> Update(
    Guid seatId,
    string fullname,
    int startStopOrder,
    int endStopOrder,
    decimal price)
    {

        if (seatId == Guid.Empty)
        {
            return TicketErrors.SeatIdRequired;
        }

        if (string.IsNullOrWhiteSpace(fullname))
        {
            return TicketErrors.FullNameRequired;
        }

        if (startStopOrder < 0)
        {
            return TicketErrors.StartStopOrderMustBePositive;
        }

        if (endStopOrder <= 0)
        {
            return TicketErrors.EndStopOrderMustBePositive;
        }

        if (price <= 0)
        {
            return TicketErrors.PriceMustBePositive;
        }

        if (decimal.Remainder(price * 100, 1) != 0)
        {
            return TicketErrors.PriceTooManyDecimalPlaces;
        }

        if (price > 9999999999999999.99m)
        {
            return TicketErrors.PriceExceedsLimit;
        }


        SeatId = seatId;
        Fullname = fullname;
        StartStopOrder = startStopOrder;
        EndStopOrder = endStopOrder;
        Price = price;

        return Result.Updated;

    }

}
