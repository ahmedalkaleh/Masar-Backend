using Masar.Domain.Trains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Dtos
{
    public class TrainDto
    {
        public Guid TrainID { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string TrainType { get; set; } = string.Empty;
        public int MaxSpeedKmh { get; set; }
        public Guid? CurrentStationId { get; set; }

    }
}
