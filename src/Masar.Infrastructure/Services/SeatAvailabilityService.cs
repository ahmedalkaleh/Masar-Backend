
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Bookings;
using Masar.Domain.Carriages;
using Masar.Domain.Seats;
using Masar.Domain.Tickets;
using Masar.Domain.Trips;
using Microsoft.EntityFrameworkCore;

namespace Masar.Infrastructure.Services;

public sealed class SeatAvailabilityService(
    IAppDbContext context) : ISeatAvailabilityService
{
    private readonly IAppDbContext _context = context;

    private sealed record RouteBounds(int Start, int End);

    private async Task<RouteBounds?> GetRouteBoundsAsync(
        Guid tripId,
        Guid boardingStationId,
        Guid alightingStationId,
        CancellationToken cancellationToken)
    {
        if (boardingStationId == alightingStationId)
            return null;

        var trip = await _context.Trips
            .AsNoTracking()
            .Include(t => t.TripStops)
            .FirstOrDefaultAsync(
                t => t.Id == tripId,
                cancellationToken);

        if (trip is null ||
            trip.Status == TripStatus.Cancelled ||
            trip.Status == TripStatus.Completed)
        {
            return null;
        }

        var stops = trip.TripStops
            .Where(s => !s.IsDelete)
            .OrderBy(s => s.StopOrder)
            .ToList();

        // Origin = 0, intermediate stops = 1..N,
        // destination = N + 1.
        var stationOrder = new Dictionary<Guid, int>
        {
            [trip.OriginStationId] = 0
        };

        for (var i = 0; i < stops.Count; i++)
        {
            // This implementation assumes stop orders are
            // consecutive: 1, 2, 3, ...
            if (stops[i].StopOrder != i + 1)
                return null;

            if (!stationOrder.TryAdd(
                    stops[i].StationId,
                    stops[i].StopOrder))
            {
                // A station must not occur twice on this trip.
                return null;
            }
        }

        if (!stationOrder.TryAdd(
                trip.DestinationStationId,
                stops.Count + 1))
        {
            return null;
        }

        if (!stationOrder.TryGetValue(
                boardingStationId, out var start))
        {
            return null;
        }

        if (!stationOrder.TryGetValue(
                alightingStationId, out var end))
        {
            return null;
        }

        if (end <= start)
            return null;

        return new RouteBounds(start, end);
    }

    private IQueryable<Ticket> GetBlockingTicketsQuery(
        Guid seatId,
        int start,
        int end)
    {
        var now = DateTime.UtcNow;

        return _context.Tickets
            .Where(ticket =>
                ticket.SeatId == seatId &&
                ticket.StartStopOrder < end &&
                start < ticket.EndStopOrder &&
                (
                    ticket.Status == TicketStatus.Pending ||
                    ticket.Status == TicketStatus.Confirmed ||
                    ticket.Status == TicketStatus.Used
                ) &&
                (
                    ticket.Booking.PaymentStatus == PaymentStatus.Paid ||
                    (
                        ticket.Booking.PaymentStatus == PaymentStatus.Pending &&
                        ticket.Booking.ExpiresAt > now
                    )
                ));
    }

    public async Task<bool> IsSeatAvailableAsync(
        Guid tripId,
        Guid seatId,
        Guid boardingStationId,
        Guid alightingStationId,
        CancellationToken cancellationToken)
    {
        var bounds = await GetRouteBoundsAsync(
            tripId,
            boardingStationId,
            alightingStationId,
            cancellationToken);

        if (bounds is null)
            return false;

        var seatBelongsToTrip = await _context.Seats
            .AnyAsync(
                seat =>
                    seat.Id == seatId &&
                    seat.IsActive &&
                    seat.Carriage.Train.Trips.Any(
                        trip => trip.Id == tripId),
                cancellationToken);

        if (!seatBelongsToTrip)
            return false;

        var hasBlockingTicket = await GetBlockingTicketsQuery(
                seatId,
                bounds.Start,
                bounds.End)
            .AnyAsync(cancellationToken);

        return !hasBlockingTicket;
    }

    public async Task<IReadOnlyList<AvailableSeatDto>>
        GetAvailableSeatsAsync(
            Guid tripId,
            Guid boardingStationId,
            Guid alightingStationId,
            CancellationToken cancellationToken)
    {
        var bounds = await GetRouteBoundsAsync(
            tripId,
            boardingStationId,
            alightingStationId,
            cancellationToken);

        if (bounds is null)
            return Array.Empty<AvailableSeatDto>();

        var now = DateTime.UtcNow;

        var seats = await _context.Seats
            .AsNoTracking()
            .Where(seat =>
                seat.IsActive &&
                seat.Carriage.Train.Trips.Any(
                    trip => trip.Id == tripId))
            .Where(seat =>
                !_context.Tickets.Any(ticket =>
                    ticket.SeatId == seat.Id &&
                    ticket.StartStopOrder < bounds.End &&
                    bounds.Start < ticket.EndStopOrder &&
                    (
                        ticket.Status == TicketStatus.Pending ||
                        ticket.Status == TicketStatus.Confirmed ||
                        ticket.Status == TicketStatus.Used
                    ) &&
                    (
                        ticket.Booking.PaymentStatus == PaymentStatus.Paid ||
                        (
                            ticket.Booking.PaymentStatus == PaymentStatus.Pending &&
                            ticket.Booking.ExpiresAt > now
                        )
                    )))
            .Select(seat => new AvailableSeatDto(
                seat.Id,
                seat.SeatNumber,
                seat.RowNumber,
                seat.ColumnNumber,
                seat.SeatType,
                seat.Carriage.CarriageNumber,
                seat.Carriage.ClassType))
            .ToListAsync(cancellationToken);

        return seats;
    }
}
