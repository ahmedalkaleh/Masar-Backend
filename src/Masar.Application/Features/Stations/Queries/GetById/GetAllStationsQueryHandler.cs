using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Stations.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Stations.Queries.GetById
{
    public sealed class GetAllStationsQueryHandler(IAppDbContext context, ILogger<GetStationByIdQuery> logger) : IRequestHandler<GetStationByIdQuery, Result<StationDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger _logger = logger;

        public async Task<Result<StationDto>> Handle(GetStationByIdQuery query, CancellationToken cancellationToken)
        {
            var station = await _context.Stations
                .AsNoTracking()
                .Select(x =>
                new StationDto
                {
                    StationID = x.Id,
                    NameAr = x.NameAr,
                    NameEn = x.NameEn,
                    Governorate = x.Governorate,
                    Type = x.Type,
                    Latitude = x.Latitude,
                    Longitude = x.Longitude,
                    CustomsDelayMinutes = x.CustomsDelayMinutes
                })
                .FirstOrDefaultAsync(x => x.StationID == query.Id, cancellationToken);

            if (station == null)
            {
                _logger.LogWarning("Station get aborted. Station with id {StationId} not found.", query.Id);
                return StationErrors.StationNotFound;
            }

            return station;
        }

    }
}
