using FluentValidation;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Commands.UpdatePerson;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerCommandValidator : AbstractValidator<UpdatePassengerCommand>
    {
        public UpdatePassengerCommandValidator(IValidator<CreatePersonCommand> personValidator)
        {
            RuleFor(x => x.Person)
            .NotNull()
            .SetValidator(personValidator);

        }
    }
}
