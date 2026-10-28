
using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Seats.Dtos;
using MediatR;

namespace Masar.Application.Features.Seats.Queries.GetAvailableSeats;

public sealed class GetAvailableSeatsQueryHandler(
    ISeatAvailabilityService seatAvailabilityService)
    : IRequestHandler<
        GetAvailableSeatsQuery,
        IReadOnlyList<AvailableSeatDto>>
{
    public async Task<IReadOnlyList<AvailableSeatDto>> Handle(
        GetAvailableSeatsQuery request,
        CancellationToken cancellationToken)
    {
        return await seatAvailabilityService.GetAvailableSeatsAsync(
            request.TripId,
            request.BoardingStationId,
            request.AlightingStationId,
            cancellationToken);
    }
}
