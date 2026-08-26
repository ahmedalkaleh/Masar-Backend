using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace Masar.Application.Features.Users.Commands.UpdateUser
{
    public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(x => x.PersonId)
                .NotEmpty().WithMessage("Person ID is required.");

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters.");

            When(x => !string.IsNullOrEmpty(x.NewPassword), () =>
            {
                RuleFor(x => x.NewPassword)
                    .MinimumLength(6).WithMessage("Password must be at least 6 characters long.");
            });

            RuleFor(x => x.Role)
                .IsInEnum().WithMessage("Invalid role specified.");
        }
    }
}