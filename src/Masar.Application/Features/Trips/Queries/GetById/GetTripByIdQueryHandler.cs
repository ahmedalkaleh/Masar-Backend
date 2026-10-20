using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trips.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trips;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trips.Queries.GetById
{
    public class GetTripByIdQueryHandler(IAppDbContext context, ILogger<GetTripByIdQuery> logger) : IRequestHandler<GetTripByIdQuery, Result<TripDetailsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetTripByIdQuery> _logger = logger;

        public async Task<Result<TripDetailsDto>> Handle(GetTripByIdQuery query, CancellationToken cancellationToken)
        {
            var trip = await _context.Trips.AsNoTracking()
                .Select(x =>
                new TripDetailsDto
                {
                    TripId = x.Id,
                    OriginStationId = x.OriginStationId,
                    OriginStationNameAr = x.OriginStation.NameAr,
                    OriginStationNameEn = x.OriginStation.NameEn,
                    DestinationStationId = x.DestinationStationId,
                    DestinationStationNameAr = x.DestinationStation.NameAr,
                    DestinationStationNameEn = x.DestinationStation.NameEn,
                    DepartureTime = x.DepartureTime,
                    EstimatedArrivalTime = x.EstimatedArrivalTime,
                    TripStops = x.TripStops.Select(x => 
                    new TripStopDetailsDto
                    {
                        TripStopId = x.Id,
                        StationId = x.StationId,
                        StationNameAr = x.Station.NameAr,
                        StationNameEn = x.Station.NameEn,
                        StopOrder = x.StopOrder,
                        ScheduledDeparture = x.ScheduledDeparture,
                        ScheduledArrival = x.ScheduledArrival,
                        DwellTimeMinutes = x.DwellTimeMinutes
                    }).ToList()
                }).FirstOrDefaultAsync(x => x.TripId == query.Id, cancellationToken);

            if (trip == null)
            {
                _logger.LogWarning("Trip get aborted. Trip with id {TripId} not found.", query.Id);
                return TripErrors.TripNotFound;
            }

            return trip;
        }

    }

}
