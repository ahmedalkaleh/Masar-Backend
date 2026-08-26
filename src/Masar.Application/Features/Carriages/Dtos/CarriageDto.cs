using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Carriages.Dtos
{
    public class CarriageDto
    {
        public Guid CarriageId { get; set; }

        public Guid TrainId { get; set; }

        public int CarriageNumber { get; set; }

        public string ClassType { get; set; } = string.Empty;

        public int TotalSeats { get; set; }
    }
}
