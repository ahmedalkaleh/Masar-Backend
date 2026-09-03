using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Mappers;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
namespace Masar.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommandHandler(IAppDbContext context, ILogger<CreateStationCommandHandler> logger) : IRequestHandler<CreateStationCommand, Result<StationDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateStationCommandHandler> _logger = logger;

        public async Task<Result<StationDto>> Handle(CreateStationCommand command,CancellationToken cancellationToken)
        {
            var createStationResult = Masar.Domain.Stations.Station.Create(Guid.NewGuid(), command.NameAr, command.NameEn,
                command.Type, command.Latitude, command.Longitude, command.Governorate, command.CustomsDelayMinutes);

            if(createStationResult.IsError)
            {
                return createStationResult.Errors;
            }

            await _context.Stations.AddAsync(createStationResult.Value);

            await _context.SaveChangesAsync(cancellationToken);
            
            _logger.LogInformation("Station created successfully. Id: {StationId}", createStationResult.Value.Id);
            return createStationResult.Value.ToDto();

        }
    }
    
}
