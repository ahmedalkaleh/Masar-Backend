using Masar.Application.Features.Passengers.Dtos;
using Masar.Application.Features.Persons.Mappers;
using Masar.Domain.Passengers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Mappers
{
    public static class PassengerMapper
    {
        public static PassengerDto ToDto(this Passenger Passenger)
        {

            return new PassengerDto
            {
                PassengerID = Passenger.Id,
                person = Passenger.Person.ToDto()
            };
        }

        public static List<PassengerDto> ToDtos(this IEnumerable<Passenger> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
