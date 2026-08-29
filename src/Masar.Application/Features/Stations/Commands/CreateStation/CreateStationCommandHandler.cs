using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Mappers;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.CreateStation
{
    public class CreateStationCommandHandler(IAppDbContext context) : IRequestHandler<CreateStationCommand, Result<StationDto>>
    {
        private readonly IAppDbContext _context = context;

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

            return createStationResult.Value.ToDto();

        }
    }
    
}
