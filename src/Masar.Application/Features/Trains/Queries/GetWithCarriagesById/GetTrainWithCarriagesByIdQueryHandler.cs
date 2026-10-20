using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Queries.GetWithCarriagesById
{
    public class GetTrainWithCarriagesByIdQueryHandler(IAppDbContext context, ILogger<GetTrainWithCarriagesByIdQuery> logger) : IRequestHandler<GetTrainWithCarriagesByIdQuery, Result<TrainWithCarriagesDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetTrainWithCarriagesByIdQuery> _logger = logger;

        public async Task<Result<TrainWithCarriagesDto>> Handle(GetTrainWithCarriagesByIdQuery query, CancellationToken cancellationToken)
        {
            var train = await _context.Trains.AsNoTracking()
                .Select(x =>
                new TrainWithCarriagesDto
                {
                    TrainID = x.Id,
                    Name = x.Name,
                    Carriages = x.Carriages.Select(x =>
                    new CarriageDto
                    {
                        CarriageId = x.Id,
                        TrainId = x.Id,
                        CarriageNumber = x.CarriageNumber,
                        ClassType = x.ClassType,
                        TotalSeats = x.TotalSeats,
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.TrainID == query.Id, cancellationToken);

            if (train == null)
            {
                _logger.LogWarning("Train with carriages get aborted. Train with id {TrainId} not found.", query.Id);
                return TrainErrors.TrainNotFound;
            }

            return train;
        }

    }
}
