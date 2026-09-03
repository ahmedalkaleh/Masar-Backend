using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Masar.Application.Features.Persons.Commands.CreatePerson
{
    public sealed class CreatePersonCommandValidator:AbstractValidator<CreatePersonCommand>
    {
        public CreatePersonCommandValidator() { 
        RuleFor(x=>x.FullName).NotEmpty().WithMessage("Full name is required.");
        RuleFor(x=>x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.PhoneNumber)
         .NotEmpty().WithMessage("Phone number is required.")
         .Matches(@"^\+?\d{7,15}$").WithMessage("Phone number must be 7–15 digits and may start with '+'.");

        }
    }
}
