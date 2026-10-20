using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Carriages.Dtos;
using Masar.Application.Features.Seats.Dtos;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trips;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Queries.GetCarriagesWithSeatesByTripId
{
    public class GetTrainByTripIdQueryHandler(IAppDbContext context, ILogger<GetTrainByTripIdQuery> logger) : IRequestHandler<GetTrainByTripIdQuery, Result<TrainWithCarriagesSeatsDto>>
    {
        private readonly IAppDbContext _context = context;
        private readonly ILogger<GetTrainByTripIdQuery> _logger = logger;

        public async Task<Result<TrainWithCarriagesSeatsDto>> Handle(GetTrainByTripIdQuery query, CancellationToken cancellationToken)
        {
            var trip = await _context.Trips.FirstOrDefaultAsync(x => x.Id == query.TripId, cancellationToken); 

            if (trip == null)
            {
                _logger.LogWarning("train with carriages and seats get aborted. trip with id {TripId} not found.", query.TripId);
                return TripErrors.TripNotFound;
            }

            var q =
                from tra in _context.Trains.AsNoTracking()
                join c in _context.Carriages.AsNoTracking()
                on tra.Id equals c.TrainId
                into CarriageGroup
                from c in CarriageGroup.DefaultIfEmpty()
                join s in _context.Seats.AsNoTracking()
                on c.Id equals s.Id
                into SeatGroup
                from s in SeatGroup.DefaultIfEmpty()
                join ti in _context.Tickets.AsNoTracking()
                on s.Id equals ti.SeatId
                into TicketGroup
                from ti in TicketGroup.DefaultIfEmpty()
                join b in _context.Bookings.AsNoTracking()
                on ti.BookingId equals b.Id
                join tr in _context.Trips.AsNoTracking()
                on b.TripId equals tr.Id
                where tra.Id == trip.TrainId && tr.Id == trip.Id
                select new TrainWithCarriagesSeatsDto
                { 
                    TrainID = tr.TrainId,
                    Name = tr.Train.Name,
                    Carriages = CarriageGroup.Select(x => new CarriageSummaryDto
                {
                    CarriageId = x.Id,
                    CarriageNumber = x.CarriageNumber,
                    Seats = x.Seats.Select(x =>
                    new SeatDetailsDto
                    {
                        SeatId = x.Id,
                        SeatNumber = x.SeatNumber,
                        RowNumber = x.RowNumber,
                        ColumnNumber = x.ColumnNumber,
                        SeatType = x.SeatType,
                        IsActive = x.IsActive,
                        IsBooking = ti != null ? true : false
                    }).ToList()
                }).ToList()
                };

            var carriages = await q.FirstAsync(x => x.TrainID == trip.TrainId, cancellationToken);

            return carriages;
        }

    }




}
