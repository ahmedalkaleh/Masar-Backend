using Masar.Application.Features.Bookings.Dtos;
using Masar.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Bookings.Mappers
{
    public static class BookingMapper
    {
        public static BookingDto ToDto(this Booking Booking)
        {

            return new BookingDto
            {
                BookingID = Booking.Id,
                BookingReference = Booking.BookingReference,
                PassengerId = Booking.PassengerId,
                TripId = Booking.TripId,
                BoardingStationId = Booking.BoardingStationId,
                AlightingStationId = Booking.AlightingStationId,
                TotalPrice = Booking.TotalPrice,
                PaymentStatus = Booking.PaymentStatus,
                PaidAt = Booking.PaidAt,
                ExpiresAt = Booking.ExpiresAt
            };
        }

        public static List<BookingDto> ToDtos(this IEnumerable<Booking> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
