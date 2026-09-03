using Masar.Application.Common.Interfaces;

using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Masar.Application.Features.Trains.Commands.UpdateTrain
{
    public class UpdateTrainCommandHandler(IAppDbContext context, ILogger<UpdateTrainCommandHandler> logger) : IRequestHandler<UpdateTrainCommand, Result<Updated>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<UpdateTrainCommandHandler> _logger = logger;

        public async Task<Result<Updated>> Handle(UpdateTrainCommand request, CancellationToken cancellationToken)
        {
            var Train = await _context.Trains.FirstOrDefaultAsync(p => p.Id == request.TrainID, cancellationToken);
            if (Train is null)
            {
                _logger.LogWarning("Train update aborted. Train with id {TrainId} not found.", request.TrainID);
                return TrainErrors.TrainNotFound;
            }

            if (_context.Trains.Any(x => x.Code == request.Code && Train.Code != request.Code))
            {
                _logger.LogWarning("Train update aborted. Train with code {TrainCode} already exists.", request.Code);
                return TrainErrors.CodeAlreadyExists;
            }

            var updatedTrainResult = Train.Update(

                request.Code,
                request.Name,
                request.TrainType,
                request.MaxSpeedKmh);

            if (updatedTrainResult.IsError)
            {
                return updatedTrainResult.Errors;
            }

            var updatedTrain = updatedTrainResult.Value;

            await _context.SaveChangesAsync(cancellationToken);
            _logger.LogInformation("Train with id {TrainId} updated successfully.", request.TrainID);
            return Result.Updated;
        }
    }
}
