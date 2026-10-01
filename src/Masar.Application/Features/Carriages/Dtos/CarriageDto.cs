using Masar.Domain.Carriages;
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

        public ClassType ClassType { get; set; }

        public short TotalSeats { get; set; }
    }
}
