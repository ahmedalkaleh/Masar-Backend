using Masar.Domain.Bookings;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Bookings.Dtos
{
    public class BookingDto
    {
        public Guid BookingID { get; set; }

        public string BookingReference { get; set; } = string.Empty;

        public Guid PassengerId { get;  set; }

        public Guid TripId { get; set; }

        public Guid BoardingStationId { get; set; }

        public Guid AlightingStationId { get; set; }

        public decimal TotalPrice { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public DateTime? PaidAt { get; set; }

        public DateTime ExpiresAt { get; set; }
    }
}
