using Masar.Application.Features.Persons.Dtos;
using Masar.Application.Features.Persons.Mappers;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Persons;
using Masar.Domain.Trains;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Trains.Mappers
{
    public static class TrainMapper
    {
        public static TrainDto ToDto(this Train train)
        {

            return new TrainDto
            {
                TrainID = train.Id,
                Code = train.Code,
                Name = train.Name,
                TrainType = train.TrainType
            };
        }

        public static List<TrainDto> ToDtos(this IEnumerable<Train> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
