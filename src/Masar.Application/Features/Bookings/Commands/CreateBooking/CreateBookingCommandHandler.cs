
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Bookings.Dtos;
using Masar.Application.Features.Bookings.Mappers;
using Masar.Domain.Bookings;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using Masar.Domain.RouteTemplateStops;
using Masar.Domain.Tickets;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Bookings.Commands.CreateBooking;

public class CreateBookingCommandHandler(
    IAppDbContext context,
    ISeatAvailabilityService seatAvailabilityService,
    ILogger<CreateBookingCommandHandler> logger)
    : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
{
    private readonly IAppDbContext _context = context;
    private readonly ISeatAvailabilityService _seatAvailabilityService =
        seatAvailabilityService;
    private readonly ILogger<CreateBookingCommandHandler> _logger = logger;

    public async Task<Result<BookingDto>> Handle(
        CreateBookingCommand command,
        CancellationToken cancellationToken)
    {
        // 1. Validate passenger.
        var passengerExists = await _context.Passengers
            .AnyAsync(
                x => x.Id == command.PassengerId,
                cancellationToken);

        if (!passengerExists)
        {
            _logger.LogWarning(
                "Booking creation aborted: Passenger {PassengerId} not found.",
                command.PassengerId);

            return BookingErrors.PassengerNotFound;
        }

        // 2. Validate stations.
        if (command.BoardingStationId == command.AlightingStationId)
        {
            return BookingErrors.SameBoardingAndAlightingStation;
        }

        // 3. Validate requested tickets.
        if (command.Tickets is null || !command.Tickets.Any())
        {
            _logger.LogWarning(
                "Booking creation aborted: No tickets were provided.");

            return BookingErrors.NoTicketsProvided;
        }

        var requestedSeatIds = command.Tickets
            .Select(x => x.SeatId)
            .ToList();

        if (requestedSeatIds.Distinct().Count() != requestedSeatIds.Count)
        {
            _logger.LogWarning(
                "Booking creation aborted: Duplicate seat IDs were provided.");

            return BookingErrors.DuplicateSeat;
        }

        // 4. Load trip and its intermediate stops.
        var trip = await _context.Trips
            .AsNoTracking()
            .Include(x => x.TripStops)
            .FirstOrDefaultAsync(
                x => x.Id == command.TripId,
                cancellationToken);

        if (trip is null)
        {
            return BookingErrors.TripNotFound;
        }

        if (DateTime.UtcNow >= trip.DepartureTime)
        {
            return BookingErrors.TripHasDeparted;
        }

        // 5. Build the complete route using active stops only.
        var sortedStops = trip.TripStops
            .Where(x => !x.IsDelete)
            .OrderBy(x => x.StopOrder)
            .ToList();

        // StopOrder must be 1, 2, 3, ... for intermediate stations.
        for (var i = 0; i < sortedStops.Count; i++)
        {
            if (sortedStops[i].StopOrder != i + 1)
            {
                _logger.LogWarning(
                    "Booking creation aborted: Trip {TripId} has invalid stop ordering.",
                    command.TripId);

                return BookingErrors.InvalidRouteStationsSequence;
            }
        }

        var orderedStationIds = new List<Guid>
        {
            trip.OriginStationId
        };

        orderedStationIds.AddRange(
            sortedStops.Select(x => x.StationId));

        orderedStationIds.Add(trip.DestinationStationId);

        // 6. Find boarding and alighting positions.
        var startStop = orderedStationIds.IndexOf(
            command.BoardingStationId);

        var endStop = orderedStationIds.IndexOf(
            command.AlightingStationId);

        if (startStop < 0)
        {
            return BookingErrors.BoardingStationNotInTrip;
        }

        if (endStop < 0)
        {
            return BookingErrors.AlightingStationIdNotInTrip;
        }

        if (endStop <= startStop)
        {
            _logger.LogWarning(
                "Booking creation aborted: Invalid station sequence.");

            return BookingErrors.InvalidRouteStationsSequence;
        }

        // These values match the StopOrder convention used by the
        // seat-availability service: origin = 0, destination = N + 1.
        var startStopOrder = startStop;
        var endStopOrder = endStop;

        // 7. Calculate the price for the requested route segment.
        const decimal priceOf1Km = 1000m; // Replace with your pricing rule.

        decimal pricePerTicket = 0m;

        var routeSegments = await _context.RouteSegments
            .Where(x =>
                orderedStationIds.Contains(x.FirstStationId) &&
                orderedStationIds.Contains(x.SecondStationId))
            .ToListAsync(cancellationToken);

        for (var i = startStop; i < endStop; i++)
        {
            var fromStationId = orderedStationIds[i];
            var toStationId = orderedStationIds[i + 1];

            var segment = routeSegments.FirstOrDefault(x =>
                (x.FirstStationId == fromStationId &&
                 x.SecondStationId == toStationId) ||
                (x.FirstStationId == toStationId &&
                 x.SecondStationId == fromStationId));

            if (segment is null)
            {
                _logger.LogWarning(
                    "Route segment not found between {FromStationId} and {ToStationId}.",
                    fromStationId,
                    toStationId);

                return RouteTemplateStopErrors.RouteSegmentNotFound;
            }

            pricePerTicket += segment.DistanceKm * priceOf1Km;
        }

        // 8. Verify every requested seat is available for this trip
        // and this exact boarding-to-alighting segment.
        foreach (var requestedTicket in command.Tickets)
        {
            var isAvailable =
                await _seatAvailabilityService.IsSeatAvailableAsync(
                    command.TripId,
                    requestedTicket.SeatId,
                    command.BoardingStationId,
                    command.AlightingStationId,
                    cancellationToken);

            if (!isAvailable)
            {
                _logger.LogWarning(
                    "Booking creation aborted: Seat {SeatId} is unavailable.",
                    requestedTicket.SeatId);

                return BookingErrors.SeatNotAvailable;
            }
        }

        // 9. Create ticket objects first; do not add them to the
        // database before the booking has been created successfully.
        List<Ticket> tickets = [];

        foreach (var requestedTicket in command.Tickets)
        {
            var createTicketResult = Ticket.Create(
                Guid.NewGuid(),
                requestedTicket.SeatId,
                requestedTicket.Fullname,
                startStopOrder,
                endStopOrder,
                pricePerTicket);

            if (createTicketResult.IsError)
            {
                _logger.LogWarning(
                    "Failed to create ticket for seat {SeatId}.",
                    requestedTicket.SeatId);

                return createTicketResult.Errors;
            }

            tickets.Add(createTicketResult.Value);
        }

        // 10. Create booking with all its tickets.
        var bookingReference =
            $"PNR-{Guid.NewGuid().ToString("N")[..8].ToUpperInvariant()}";

        var totalPrice = tickets.Sum(x => x.Price);
        var expiresAt = DateTime.UtcNow.AddMinutes(5);

        var createBookingResult = Booking.Create(
            Guid.NewGuid(),
            bookingReference,
            command.PassengerId,
            command.TripId,
            command.BoardingStationId,
            command.AlightingStationId,
            totalPrice,
            expiresAt,
            tickets);

        if (createBookingResult.IsError)
        {
            return createBookingResult.Errors;
        }

        var booking = createBookingResult.Value;

        // 11. Persist the booking and its tickets together.
        // This assumes Booking.Create assigns the supplied tickets
        // to the booking's Tickets navigation property.
        await _context.Bookings.AddAsync(
            booking,
            cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Booking {BookingId} created with reference {BookingReference}.",
            booking.Id,
            booking.BookingReference);

        return booking.ToDto();
    }
}
