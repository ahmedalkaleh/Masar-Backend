using Masar.Domain.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Dtos
{
    public class SeatDto
    {
        public Guid SeatID { get; set; }

        public Guid CarriageId { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        public string RowNumber { get; set; } = string.Empty;

        public byte ColumnNumber { get; set; }

        public SeatType SeatType { get; set; }

        public bool IsActive { get; set; }

    }
}
