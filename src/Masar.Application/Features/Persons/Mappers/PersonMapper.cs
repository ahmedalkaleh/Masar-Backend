using Masar.Application.Features.Persons.Dtos;
using Masar.Domain.Persons;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Masar.Application.Features.Persons.Mappers
{
   internal static class PersonMapper
    {
        internal static PersonDto ToDto(this Masar.Domain.Persons.Person person)
        {
            if (person == null) throw new ArgumentNullException(nameof(person));
            return new PersonDto
            {
                PersonID = person.Id,
                FullName = person.FullName,
                Email = person.Email,
                PhoneNumber = person.PhoneNumber
            };
        }

        internal static List<PersonDto> ToDtos(this IEnumerable<Person> entities)
        {
            if (entities == null) throw new ArgumentNullException(nameof(entities));
            return entities.Select(e => e.ToDto()).ToList();
        }
    }
}
