using Masar.Application.Features.Seats.Dtos;

namespace Masar.Application.Features.Carriages.Dtos
{
    public class CarriageSummaryDto
    {
        public Guid CarriageId { get; set; }
        public int CarriageNumber { get; set; }

        public List<SeatDetailsDto> Seats { get; set; } = null!;
    }

}
