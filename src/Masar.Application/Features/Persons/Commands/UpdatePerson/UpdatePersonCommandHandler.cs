using Masar.Application.Common.Interfaces;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
namespace Masar.Application.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler(IAppDbContext context,ILogger<UpdatePersonCommandHandler> logger) : IRequestHandler<UpdatePersonCommand, Result<Updated>>
    {
       private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdatePersonCommandHandler> _logger= logger;
        public async Task<Result<Updated>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == request.PersonID, cancellationToken);
            if (person is null)
            {
                _logger.LogWarning("Person {PersonId} not found for update.", request.PersonID);

                return PersonErrors.PersonNotFound;
            }
            var updatedPersonResult = person.Update(
              
                request.FullName,
                request.Email,
                request.PhoneNumber);
            if (updatedPersonResult.IsError)
            {
                return updatedPersonResult.Errors;
            }
            var updatedPerson = updatedPersonResult.Value;
            
            await _context.SaveChangesAsync(cancellationToken);
            return Result.Updated;
        }
    }
}
