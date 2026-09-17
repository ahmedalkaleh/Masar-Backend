using Masar.Domain.Tickets;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Tickets.Dtos
{
    public class TicketDto
    {
        public Guid TicketID {  get; set; }

        public Guid BookingId { get; set; }

        public Guid SeatId { get; set; }

        public string Fullname { get; set; } = string.Empty;

        public int StartStopOrder { get; set; }

        public int EndStopOrder { get; set; }

        public decimal Price { get; set; }

        public string QrcodeHash { get; set; } = string.Empty;

        public TicketStatus Status { get; set; }

        public bool IsUsed { get; set; }

        public DateTime? BoardedAt { get; set; }
    }
}
