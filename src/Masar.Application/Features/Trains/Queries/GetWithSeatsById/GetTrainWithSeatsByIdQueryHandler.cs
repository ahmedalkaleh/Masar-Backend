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

            var q =
                from c in _context.Carriages
                join s in _context.Seats
                on c.Id equals s.Id
                into SeatGroup
                from s in SeatGroup.DefaultIfEmpty()
                join ti in _context.Tickets on s.Id equals ti.SeatId
                into TicketGroup
                from ti in TicketGroup.DefaultIfEmpty()
                join b in _context.Bookings on ti.BookingId equals b.Id
                join tr in _context.Trips on b.TripId equals tr.Id
                select new CarriageWithSeatsOfTripDto
                {
                    CarriageId = c.Id,
                    CarriageNumber = c.CarriageNumber,
                    Seats = SeatGroup.Select(x =>
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
                };
                
            var carriages = await q.ToListAsync(cancellationToken);

            return train;
        }

    }


}
