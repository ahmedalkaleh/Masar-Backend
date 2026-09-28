using Masar.Application.Common.Interfaces;
using Masar.Domain.Common.Results;
using Masar.Domain.Passengers;
using Masar.Domain.Persons;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.UpdatePassenger
{
    public class UpdatePassengerCommandHandler(IAppDbContext context, ILogger<UpdatePassengerCommandHandler> logger) : IRequestHandler<UpdatePassengerCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdatePassengerCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdatePassengerCommand request, CancellationToken cancellationToken)
        {

            var passenger = await _context.Passengers.Include(x => x.Person).FirstOrDefaultAsync(p => p.Id == request.PassengerID, cancellationToken);
            if (passenger is null)
            {
                _logger.LogWarning("Passenger {PassengerID} not found for update.", request.PassengerID);

                return PassengerErrors.PassengerNotFound;
            }

            var updatedPersonResult = passenger.Person.Update(
                request.Person.FullName,
                request.Person.Email,
                request.Person.PhoneNumber);

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
