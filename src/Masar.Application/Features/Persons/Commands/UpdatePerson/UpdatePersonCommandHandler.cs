using Masar.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using Masar.Domain.Common.Results;
using Microsoft.EntityFrameworkCore;
using Masar.Domain.Persons;
namespace Masar.Application.Features.Persons.Commands.UpdatePerson
{
    public class UpdatePersonCommandHandler(IAppDbContext context) : IRequestHandler<UpdatePersonCommand, Result<Updated>>
    {
       private readonly IAppDbContext _context = context;
        public async Task<Result<Updated>> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
        {
            var person = await _context.Persons.FirstOrDefaultAsync(p => p.Id == request.PersonID, cancellationToken);
            if (person is null)
            {
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
