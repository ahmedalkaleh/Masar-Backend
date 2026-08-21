using Masar.Domain.Common.Results;

namespace Masar.Domain.Persons;

public static class PersonErrors
{
    public static Error FullNameRequired =>
        Error.Validation("Person.FullNameRequired", "Full name is required.");

    public static Error EmailRequired =>
        Error.Validation("Person.EmailRequired", "Email is required.");

    public static Error InvalidEmail =>
        Error.Validation("Person.InvalidEmail", "Email address format is invalid.");

    public static Error PhoneNumberRequired =>
        Error.Validation("Person.PhoneNumberRequired", "Phone number is required.");

    public static readonly Error InvalidPhoneNumber =
        Error.Validation("Person.InvalidPhoneNumber", "Phone number must be between 7 and 15 digits and may start with '+'.");

    public static Error PersonNotFound =>
        Error.NotFound("Person.NotFound", "Person with the specified ID was not found.");

    public static Error EmailAlreadyExists =>
        Error.Conflict("Person.EmailAlreadyExists", "A person with this email already exists.");

    public static Error PhoneNumberAlreadyExists =>
        Error.Conflict("Person.PhoneNumberAlreadyExists", "A person with this phone number already exists.");
}