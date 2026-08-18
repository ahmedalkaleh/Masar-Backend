using Masar.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Person.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;

namespace Masar.Application.Features.Person.Commands.CreatePerson
{
    public class CreatePersonCommandHandler(IAppDbContext context):IRequestHandler<CreatePersonCommand , Result<PersonDto>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<PersonDto>> Handle(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var email = command.Email.Trim().ToLower();
            var createPersonResult =Person(Guid.NewGuid(), command.FullName, email, command.PhoneNumber);
        }
    }
}
