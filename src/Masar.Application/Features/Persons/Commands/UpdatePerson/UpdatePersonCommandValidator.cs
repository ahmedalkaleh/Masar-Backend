using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
namespace Masar.Application.Features.Persons.Commands.UpdatePerson
{
   public sealed class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
    {
        public UpdatePersonCommandValidator() {

            RuleFor(x => x.PersonID)
               .NotEmpty().WithMessage("Person ID is required.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Full name is required.")
                .MinimumLength(2).WithMessage("Full name must be at least 2 characters.")
                .MaximumLength(150).WithMessage("Full name must not exceed 150 characters.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Invalid email format.")
                .MaximumLength(150)
                .WithMessage("Email must not exceed 150 characters.");


            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required.")
                .Matches(@"^\+[1-9]\d{6,14}$")
                .WithMessage("Phone number must be 7–15 digits and may start with '+'.");


        }
    }
}
