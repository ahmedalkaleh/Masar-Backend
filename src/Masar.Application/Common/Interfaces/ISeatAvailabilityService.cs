using System;
using System.Collections.Generic;
using System.Text;
using Masar.Application.Features.Seats.Dtos;
namespace Masar.Application.Common.Interfaces
{
    public interface ISeatAvailabilityService
    {
        Task<bool> IsSeatAvailableAsync(
            Guid tripId,
            Guid seatId,
            Guid boardingStationId,
            Guid alightingStationId,
            CancellationToken cancellationToken);

        Task<IReadOnlyList<AvailableSeatDto>> GetAvailableSeatsAsync(
            Guid tripId,
            Guid boardingStationId,
            Guid alightingStationId,
            CancellationToken cancellationToken);
    }
}
