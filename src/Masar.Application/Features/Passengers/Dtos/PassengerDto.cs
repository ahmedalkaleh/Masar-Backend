using Masar.Application.Features.Persons.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Dtos
{
    public class PassengerDto
    {
        public Guid PassengerID {  get; set; }

        public PersonDto person { get; set; } = null!;
    }
}
