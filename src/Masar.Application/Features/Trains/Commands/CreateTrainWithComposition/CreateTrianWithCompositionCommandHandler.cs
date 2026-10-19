
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Dtos;
using Masar.Application.Features.Trains.Mappers;
using Masar.Domain.Carriages;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Commands.CreateTrainWithComposition;

public sealed class CreateTrainWithCompositionCommandHandler(
    IAppDbContext context,
    ILogger<CreateTrainWithCompositionCommandHandler> logger)
    : IRequestHandler<CreateTrainWithCompositionCommand, Result<TrainDto>>
{
    private readonly IAppDbContext _context = context;
    private readonly ILogger<CreateTrainWithCompositionCommandHandler> _logger = logger;

    public async Task<Result<TrainDto>> Handle(
        CreateTrainWithCompositionCommand command,
        CancellationToken cancellationToken)
    {
        var code = command.Code.Trim();

        if (!await _context.Stations.AnyAsync(
                x => x.Id == command.CurrentStationId,
                cancellationToken))
        {
            _logger.LogWarning(
                "Train creation aborted. Station {StationId} was not found.",
                command.CurrentStationId);

            return TrainErrors.CurrentStationNotFound;
        }

        if (await _context.Trains.AnyAsync(
                x => x.Code == code,
                cancellationToken))
        {
            _logger.LogWarning(
                "Train creation aborted. Code {TrainCode} already exists.",
                code);

            return TrainErrors.CodeAlreadyExists;
        }

        var trainId = Guid.NewGuid();

        var trainResult = Train.Create(
            trainId,
            code,
            command.Name.Trim(),
            command.TrainType.Trim(),
            command.MaxSpeedKmh,
            command.CurrentStationId);

        if (trainResult.IsError)
            return trainResult.Errors;

        var train = trainResult.Value;

        await _context.Trains.AddAsync(train, cancellationToken);

        foreach (var carriageInput in command.Carriages)
        {
            var seats = carriageInput.Seats ?? [];

            // TotalSeats is calculated by the backend.
            var totalSeats = checked((short)seats.Count);

            var carriageId = Guid.NewGuid();

            var carriageResult = Carriage.Create(
                carriageId,
                trainId,
                carriageInput.CarriageNumber,
                carriageInput.ClassType,
                totalSeats);

            if (carriageResult.IsError)
                return carriageResult.Errors;

            await _context.Carriages.AddAsync(
                carriageResult.Value,
                cancellationToken);

            foreach (var seatInput in seats)
            {
                var seatResult = Seat.Create(
                    Guid.NewGuid(),
                    carriageId,
                    seatInput.RowNumber.Trim(),
                    seatInput.ColumnNumber,
                    seatInput.SeatType);

                if (seatResult.IsError)
                    return seatResult.Errors;

                await _context.Seats.AddAsync(
                    seatResult.Value,
                    cancellationToken);
            }
        }

        // Persist the complete composition in one SaveChanges call.
        // EF Core normally wraps a relational SaveChanges operation
        // in a transaction.
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation(
            "Train {TrainCode} and its composition were created successfully.",
            train.Code);

        return train.ToDto();
    }
}
