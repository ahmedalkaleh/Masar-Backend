using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Commands.CreateTrain;
using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Mappers;
using Masar.Domain.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Masar.Domain.Trains;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Features.Trains.Commands.CreateTrain
{
    public class CreateTrainCommandHandler(IAppDbContext context, ILogger<CreateTrainCommandHandler> logger) : IRequestHandler<CreateTrainCommand, Result<TrainDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<CreateTrainCommandHandler> _logger = logger;

        public async Task<Result<TrainDto>> Handle(CreateTrainCommand command, CancellationToken cancellationToken)
        {
            if (! await _context.Stations.AnyAsync(x => x.Id == command.CurrentStationId, cancellationToken))
            {
                _logger.LogWarning("Train Creation aborted. Train with Current Station Id {CurrentStationId} not found.", command.CurrentStationId);
                return TrainErrors.CurrentStationNotFound;
            }

            var code = command.Code.Trim();

            if (await _context.Trains.AnyAsync(x => x.Code == code, cancellationToken))
            {
                _logger.LogWarning("Train Creation aborted. Train with code {TrainCode} already exists.", command.Code);
                return TrainErrors.CodeAlreadyExists;
            }

            var createTrainResult = Masar.Domain.Trains.Train.Create(Guid.NewGuid(), code, command.Name.Trim(), command.TrainType.Trim(), command.MaxSpeedKmh,command.CurrentStationId);
            if (createTrainResult.IsError)
            {
                return createTrainResult.Errors;
            }

            await _context.Trains.AddAsync(createTrainResult.Value, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            var Train = createTrainResult.Value;
            _logger.LogInformation("Train with code {TrainCode} created successfully.", Train.Code);
            return Train.ToDto();
        }
    }
}
