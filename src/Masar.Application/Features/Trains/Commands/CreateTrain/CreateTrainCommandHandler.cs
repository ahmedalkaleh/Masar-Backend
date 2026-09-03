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

namespace Masar.Application.Features.Trains.Commands.CreateTrain
{
    public class CreateTrainCommandHandler(IAppDbContext context, ILogger logger) : IRequestHandler<CreateTrainCommand, Result<TrainDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger _logger = logger;

        public async Task<Result<TrainDto>> Handle(CreateTrainCommand command, CancellationToken cancellationToken)
        {

            if(_context.Trains.Any(x => x.Code == command.Code))
            {
                _logger.LogWarning("Train Creation aborted. Train with code {TrainCode} already exists.", command.Code);
                return TrainErrors.CodeAlreadyExists;
            }

            var createTrainResult = Masar.Domain.Trains.Train.Create(Guid.NewGuid(), command.Code.Trim(), command.Name.Trim(), command.TrainType.Trim(), command.MaxSpeedKmh,command.CurrentStationId);
            if (createTrainResult.IsError)
            {
                return createTrainResult.Errors;
            }

            _context.Trains.Add(createTrainResult.Value);

            await _context.SaveChangesAsync(cancellationToken);

            var Train = createTrainResult.Value;
            _logger.LogInformation("Train with code {TrainCode} created successfully.", Train.Code);
            return Train.ToDto();
        }
    }
}
