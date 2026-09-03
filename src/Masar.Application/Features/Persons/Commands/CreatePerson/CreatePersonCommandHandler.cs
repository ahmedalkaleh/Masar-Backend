using Masar.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Persons.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;
using Masar.Application.Features.Persons.Mappers;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Persons.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(IAppDbContext context, ILogger<CreatePersonCommandHandler> logger) :IRequestHandler<CreatePersonCommand , Result<PersonDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreatePersonCommandHandler> _logger = logger;

        public async Task<Result<PersonDto>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
        
            var createPersonResult =Masar.Domain.Persons.Person.Create(Guid.NewGuid(), command.FullName.Trim(), command.Email.Trim().ToLower(), command.PhoneNumber.Trim());
            if (createPersonResult.IsError)
            {
                return createPersonResult.Errors;
            }

            _context.Persons.Add(createPersonResult.Value);

            await _context.SaveChangesAsync(cancellationToken);

            var person = createPersonResult.Value;
            _logger.LogInformation("Person created successfully. Id: {PersonId}", createPersonResult.Value.Id);

            return person.ToDto();
        }
    }
}
