using Masar.Application.Features.Carriages.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Dtos
{
    public class TrainWithCarriagesSeatsDto
    {
        public Guid TrainID { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<CarriageSummaryDto> Carriages { get; set; } = null!;
    }
}
