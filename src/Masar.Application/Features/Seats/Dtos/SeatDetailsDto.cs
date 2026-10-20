using Masar.Domain.Seats;

namespace Masar.Application.Features.Seats.Dtos
{
    public class SeatDetailsDto
    {
        public Guid SeatId { get; set; }

        public string SeatNumber { get; set; } = string.Empty;

        public string RowNumber { get; set; } = string.Empty;

        public byte ColumnNumber { get; set; }

        public SeatType SeatType { get; set; }

        public bool IsActive { get; set; }

        public bool IsBooking { get; set; }

    }
}
