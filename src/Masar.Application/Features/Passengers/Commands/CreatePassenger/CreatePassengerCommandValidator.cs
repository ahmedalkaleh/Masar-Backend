using FluentValidation;
using Masar.Application.Features.Passengers.Dtos;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerCommandValidator : AbstractValidator<CreatePassengerCommand>
    {
        public CreatePassengerCommandValidator(IValidator<CreatePersonCommand> personValidator)
        {
            RuleFor(x => x.Person)
            .NotNull()
            .SetValidator(personValidator);

        }
    }
}
