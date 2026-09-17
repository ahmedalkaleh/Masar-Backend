using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Bookings.Dtos;
using Masar.Application.Features.Bookings.Mappers;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Dtos;
using Masar.Domain.Bookings;
using Masar.Domain.Common.Results;
using Masar.Domain.RouteTemplates;
using Masar.Domain.RouteTemplateStops;
using Masar.Domain.Tickets;
using Masar.Domain.TripStops;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Masar.Application.Features.Bookings.Commands.CreateBooking
{
    public class CreateBookingCommandHandler(IAppDbContext context, ILogger<CreateBookingCommandHandler> logger) : IRequestHandler<CreateBookingCommand, Result<BookingDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateBookingCommandHandler> _logger = logger;

        public async Task<Result<BookingDto>> Handle(CreateBookingCommand command, CancellationToken cancellationToken)
        {
            if (!_context.Passengers.Any(x => x.Id == command.PassengerId))
            {
                _logger.LogWarning("Booking creation aborted: Passenger with ID '{PassengerId}' not found.", command.PassengerId);
                return BookingErrors.PassengerNotFound;
            }

            if (!_context.Stations.Any(x => x.Id == command.BoardingStationId))
            {
                _logger.LogWarning("Booking creation aborted: Boarding station with ID '{BoardingStationId}' not found.", command.BoardingStationId);
                return BookingErrors.BoardingStationNotFound;
            }

            if (!_context.Stations.Any(x => x.Id == command.AlightingStationId))
            {
                _logger.LogWarning("Booking creation aborted: Alighting station with ID '{AlightingStationId}' not found.", command.AlightingStationId);
                return BookingErrors.AlightingStationNotFound;
            }

            if (command.BoardingStationId == command.AlightingStationId)
            {
                _logger.LogWarning("Booking creation aborted: Boarding station ID '{BoardingStationId}' is the same as alighting station ID '{AlightingStationId}'.", command.BoardingStationId, command.AlightingStationId);
                return BookingErrors.SameBoardingAndAlightingStation;
            }        

            var trip = await _context.Trips.AsNoTracking().Include(tr => tr.TripStops)
                .FirstOrDefaultAsync(x => x.Id == command.TripId);

            if(trip is null)
            {
                _logger.LogWarning("Booking creation aborted: Trip with ID '{TripId}' not found.", command.TripId);
                return BookingErrors.TripNotFound;
            }

            if(!trip.TripStops.Any(x => x.Id == command.BoardingStationId) && command.BoardingStationId != trip.OriginStationId)
            {
                _logger.LogWarning("The selected boarding station ID '{BoardingStationId}' is not part of the specified trip with the specified ID '{TripId}'.", command.BoardingStationId, command.TripId);
                return BookingErrors.BoardingStationNotInTrip;
            }

            if (!trip.TripStops.Any(x => x.Id == command.AlightingStationId && command.AlightingStationId != trip.DestinationStationId))
            {
                _logger.LogWarning("The selected alighting station ID '{AlightingStationId}' is not part of the specified trip with the specified ID '{TripId}'.", command.AlightingStationId, command.TripId);
                return BookingErrors.AlightingStationIdNotInTrip;
            }


            int startStop = (command.BoardingStationId == trip.OriginStationId) ?
                0 :
                trip.TripStops.First(x => x.StationId == command.BoardingStationId).StopOrder;


            int endStop = command.AlightingStationId == trip.DestinationStationId ?
                trip.TripStops.Count() + 1 :
                trip.TripStops.First(x => x.StationId == command.AlightingStationId).StopOrder;


            var sortedStops = trip.TripStops.OrderBy(x => x.StopOrder).ToList();

            decimal priceOf1KM = 1000m; // Changable

            decimal calculatePrice = 0m;

            Guid prevStationId = Guid.Empty;
            Guid currentStationId = Guid.Empty;

            for(int i = startStop + 1; i <= endStop; i++)
            {
                prevStationId = (i - 1) == 0 ? command.BoardingStationId : sortedStops[i - 1].StationId;
                
                currentStationId = sortedStops[i].StationId;

                if(i == endStop)
                    currentStationId = (endStop > sortedStops.Count()) ? command.AlightingStationId : currentStationId = sortedStops[i].StationId;

                var existRoutSegment = await _context.RouteSegments.FirstOrDefaultAsync(x =>
                    (prevStationId == x.FirstStationId && currentStationId == x.SecondStationId) ||
                    (prevStationId == x.SecondStationId && currentStationId == x.FirstStationId));


                if(existRoutSegment == null)
                {
                    _logger.LogWarning("Booking creation aborted: Route segment from station ID '{PrevStationId}' to  station ID '{CurrentStationId}' does not exist.", prevStationId, currentStationId);
                    return RouteTemplateStopErrors.RouteSegmentNotFound;
                }

                calculatePrice += (existRoutSegment.DistanceKm) * priceOf1KM;

            }

            List<Ticket> tickets = [];

            foreach(var ticket in command.Tickets)
            {
                var createTicketresult = Masar.Domain.Tickets.Ticket.Create(Guid.NewGuid(), ticket.SeatId, ticket.Fullname, startStop, endStop, calculatePrice);

                if(createTicketresult.IsError)
                {
                    _logger.LogWarning("Booking creation aborted: Failed to create Ticket for seat ID '{SeatId}'.", ticket.SeatId);
                    return createTicketresult.Errors;
                }
            }

            var bookingReference = $"PNR-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}";
            decimal totalPrice = tickets.Sum(x => x.Price);
            var expiresAt = DateTime.UtcNow.AddMinutes(5);  // Changable


            var createBookingresult = Masar.Domain.Bookings.Booking.Create(
                Guid.NewGuid(),
                bookingReference,
                command.PassengerId,
                command.TripId,
                command.BoardingStationId,
                command.AlightingStationId,
                totalPrice,
                expiresAt,
                tickets);

            if(createBookingresult.IsError)
            {
                return createBookingresult.Errors;
            }

            _logger.LogInformation("Booking created successfully with ID '{BookingId}', BookingReference {BookingReference}'.", createBookingresult.Value.Id, createBookingresult.Value.BookingReference);

            return createBookingresult.Value.ToDto();
        }

    }
}
