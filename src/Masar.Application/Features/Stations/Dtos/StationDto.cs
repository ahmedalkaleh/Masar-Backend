using Masar.Domain.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Dtos
{
    public class StationDto
    {
        public Guid StationID { get; set; }
        public string NameAr { get; set; } = String.Empty;

        public string NameEn { get; set; } = String.Empty;

        public StationType Type { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public string Governorate { get; set; } = String.Empty;

        public int CustomsDelayMinutes { get; set; }
    }
}
