using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Seats.Dtos;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Queries.GetWithSeatesById
{
    public class GetTrainWithSeatsByIdQueryHandler(IAppDbContext context, ILogger<GetTrainWithSeatsByIdQuery> logger) : IRequestHandler<GetTrainWithSeatsByIdQuery, Result<TrainWithSeatsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetTrainWithSeatsByIdQuery> _logger = logger;

        public async Task<Result<TrainWithSeatsDto>> Handle(GetTrainWithSeatsByIdQuery query, CancellationToken cancellationToken)
        {
            var train = await _context.Trains.AsNoTracking()
                .Select(x =>
                new TrainWithSeatsDto
                {
                    TrainID = x.Id,
                    Name = x.Name,
                    Seates = x.Carriages.SelectMany(x =>
                    x.Seats.Select(x =>
                    new SeatDto
                    {
                        SeatID = x.Id,
                        CarriageId = x.Id,
                        SeatNumber = x.SeatNumber,
                        RowNumber = x.RowNumber,
                        ColumnNumber = x.ColumnNumber,
                        SeatType = x.SeatType,
                        IsActive = x.IsActive
                    })).ToList()
                }).FirstOrDefaultAsync(x => x.TrainID == query.Id, cancellationToken);

            if (train == null)
            {
                _logger.LogWarning("Train with seates get aborted. Train with id {TrainId} not found.", query.Id);
                return TrainErrors.TrainNotFound;
            }

            
            return train;
        }

    }


}
