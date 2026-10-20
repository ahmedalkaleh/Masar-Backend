using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Features.Trips.Queries.GetAll
{
    public class GetAllTripsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllTripsQuery, Result<List<TripSummaryDto>>>
    {
        private readonly IAppDbContext _context = context;
        public async Task<Result<List<TripSummaryDto>>> Handle(GetAllTripsQuery query, CancellationToken cancellationToken)
        {
            var trips = await _context.Trips.AsNoTracking()
                .Where(x => x.Status == Domain.Trips.TripStatus.Scheduled && x.DepartureTime >= DateTime.UtcNow)
                .Select(x =>
                new TripSummaryDto
                {
                    TripId = x.Id,
                    OriginStationId = x.OriginStationId,
                    OriginStationNameAr = x.OriginStation.NameAr,
                    OriginStationNameEn = x.OriginStation.NameEn,
                    DestinationStationId = x.DestinationStationId,
                    DestinationStationNameAr = x.DestinationStation.NameAr,
                    DestinationStationNameEn = x.DestinationStation.NameEn,
                })
                .OrderBy(x => x.OriginStationNameAr)
                .ThenBy(x => x.DestinationStationNameAr)
                .ToListAsync(cancellationToken);

            return trips;
        }

    }



}
