using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Queries.GetById
{
    public class GetTrainByIdQueryHandler(IAppDbContext context, ILogger<GetTrainByIdQuery> logger) : IRequestHandler<GetTrainByIdQuery, Result<TrainDetailsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetTrainByIdQuery> _logger = logger;

        public async Task<Result<TrainDetailsDto>> Handle(GetTrainByIdQuery query, CancellationToken cancellationToken)
        {
            var train = await _context.Trains.AsNoTracking()
                .Select(x => 
                new TrainDetailsDto
                {
                    TrainID = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    TrainType = x.TrainType,
                    MaxSpeedKmh = x.MaxSpeedKmh,
                    Status = x.Status,
                    CurrentStationId = x.CurrentStationId,
                    CurrentStationNameAr = x.CurrentStation == null ? null :  x.CurrentStation.NameAr,
                    CurrentStationNameEn = x.CurrentStation == null ? null : x.CurrentStation.NameEn,
                }).FirstOrDefaultAsync(x => x.TrainID == query.Id, cancellationToken);

            if(train == null)
            {
                _logger.LogWarning("Train get aborted. Train with id {TrainId} not found.", query.Id);
                return TrainErrors.TrainNotFound;
            }

            return train;
        }

    }
}
