using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Passengers.Dtos;
using Masar.Application.Features.Passengers.Mappers;
using Masar.Application.Features.Persons.Commands.CreatePerson;
using Masar.Application.Features.Persons.Dtos;
using Masar.Application.Features.Persons.Mappers;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Passengers.Commands.CreatePassenger
{
    public class CreatePassengerCommandHandler(IAppDbContext context, ILogger<CreatePassengerCommandHandler> logger) : IRequestHandler<CreatePassengerCommand, Result<PassengerDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreatePassengerCommandHandler> _logger = logger;

        public async Task<Result<PassengerDto>> Handle(CreatePassengerCommand command, CancellationToken cancellationToken)
        {

            var createPersonResult = Masar.Domain.Persons.Person.Create(Guid.NewGuid(), command.Person.FullName.Trim(), command.Person.Email.Trim().ToLower(), command.Person.PhoneNumber.Trim());
            if (createPersonResult.IsError)
            {
                return createPersonResult.Errors;
            }

            var createPassengerResult = Masar.Domain.Passengers.Passenger.Create(Guid.NewGuid(), createPersonResult.Value);

            if (createPassengerResult.IsError)
            {
                return createPassengerResult.Errors;
            }

            _context.Passengers.Add(createPassengerResult.Value);

            await _context.SaveChangesAsync(cancellationToken);

            var passenger = createPassengerResult.Value;
            _logger.LogInformation("Passenger created successfully. Id: {PassengerId}", passenger.Id);

            return passenger.ToDto();
        }
    }
}
