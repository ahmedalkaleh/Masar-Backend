using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Persons.Dtos
{
    public class PersonDto
    {
        public Guid PersonID { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
