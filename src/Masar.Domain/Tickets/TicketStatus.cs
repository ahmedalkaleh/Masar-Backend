using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Tickets
{
    public enum TicketStatus
    {
        Pending,
        Confirmed,
        Used,
        Expired,
        Cancelled
    }
}
