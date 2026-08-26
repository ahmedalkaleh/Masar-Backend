using Masar.Application.Features.Carriages.Dtos;
using Masar.Domain.Carriages;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Masar.Application.Features.Carriages.Mappers
{
    public static class CarriageMapper
    {
        public static CarriageDto ToDto(this Carriage carriage)
        {
            return new CarriageDto
            {
                CarriageId = carriage.Id,
                TrainId = carriage.TrainId,
                CarriageNumber = carriage.CarriageNumber,
                ClassType = carriage.ClassType,
                TotalSeats = carriage.TotalSeats
            };
        }

        public static List<CarriageDto> ToDto(this IEnumerable<Carriage> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }


    }
}
