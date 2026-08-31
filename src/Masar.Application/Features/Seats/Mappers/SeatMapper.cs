using Masar.Application.Features.Seats.Dtos;
using Masar.Domain.Seats;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Seats.Mappers
{
    public static class SeatMapper
    {
        public static SeatDto ToDto(this Seat seat)
        {
            return new SeatDto
            {
                SeatID = seat.Id,
                CarriageId = seat.CarriageId,
                RowNumber = seat.RowNumber,
                ColumnNumber = seat.ColumnNumber,
                SeatNumber = seat.SeatNumber,
                SeatType = seat.SeatType,
                IsActive = seat.IsActive
            };
        }

        public static List<SeatDto> ToDto(this IEnumerable<Seat> entities)
        {
            return entities.Select(x => x.ToDto()).ToList();
        }
    }
}
