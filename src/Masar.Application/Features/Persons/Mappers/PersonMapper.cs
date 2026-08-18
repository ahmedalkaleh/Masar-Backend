using Masar.Application.Features.Persons.Dtos;
using Masar.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Masar.Application.Features.Persons.Mappers
{
    public static class PersonMapper
    {
        public static PersonDto ToDto(this Person person)
        {
            
            return new PersonDto
            {
                PersonID = person.Id,
                FullName = person.FullName,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber
            };
        }

        public static List<PersonDto> ToDtos(this IEnumerable<Person> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
