
using Masar.Application.Features.Seats.Dtos;
using MediatR;

namespace Masar.Application.Features.Seats.Queries.GetAvailableSeats;

public sealed record GetAvailableSeatsQuery(
    Guid TripId,
    Guid BoardingStationId,
    Guid AlightingStationId
) : IRequest<IReadOnlyList<AvailableSeatDto>>;
