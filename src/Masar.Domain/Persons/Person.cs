using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Infrastructure;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Masar.Domain.Persons;

public sealed class Person : AuditableEntity
{
    public string FullName { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PhoneNumber { get; private set; } = null!;

    private readonly List<User> _users = [];
    public IEnumerable<User> Users => _users.AsReadOnly();

    private Person() { }

    private Person(
        Guid id,
        string fullName,
        string email,
        string phoneNumber,
        List<User>? users = null)
        : base(id)
    {
        FullName = fullName;
        Email = email;
        PhoneNumber = phoneNumber;
        _users = users ?? [];
    }

    public static Result<Person> Create(
        Guid id,
        string fullName,
        string email,
        string phoneNumber,
        List<User>? users = null)
    {
        if (string.IsNullOrWhiteSpace(fullName))
        {
            return PersonErrors.FullNameRequired;
        }

        if (string.IsNullOrWhiteSpace(email))
        {
            return PersonErrors.EmailRequired;
        }

        try
        {
            _ = new MailAddress(email);
        }
        catch
        {
            return PersonErrors.InvalidEmail;
        }

        if (string.IsNullOrWhiteSpace(phoneNumber) || !Regex.IsMatch(phoneNumber, @"^\+?\d{7,15}$"))
        {
            return PersonErrors.InvalidPhoneNumber;
        }

        return new Person(id, fullName, email, phoneNumber, users);
    }
}