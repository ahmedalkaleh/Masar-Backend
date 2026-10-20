using Masar.Application.Features.Seats.Dtos;

namespace Masar.Application.Features.Trains.Dtos
{
    public class TrainWithSeatsDto
    {
        public Guid TrainID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<SeatDto> Seates { get; set; } = null!;
    }
}
