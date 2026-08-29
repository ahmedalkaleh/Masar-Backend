using Masar.Application.Features.Stations.Dtos;
using Masar.Domain.Stations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Mappers
{
    public static class StationMapper
    {
        public static StationDto ToDto(this Station station)
        {
            return new StationDto
            {
                NameAr = station.NameAr,
                NameEn = station.NameEn,
                Type = station.Type,
                Latitude = station.Latitude,
                Longitude = station.Longitude,
                Governorate = station.Governorate,
                CustomsDelayMinutes = station.CustomsDelayMinutes

            };
        }


        public static List<StationDto> ToDto(this IEnumerable<Station> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(x => x.ToDto()).ToList();
        }

    }
}
