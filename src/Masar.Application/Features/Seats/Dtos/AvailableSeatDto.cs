using Masar.Domain.Carriages;
using Masar.Domain.Seats;

namespace Masar.Application.Features.Seats.Dtos;

public sealed record AvailableSeatDto(
    Guid SeatId,
    string SeatNumber,
    string RowNumber,
    byte ColumnNumber,
    SeatType SeatType,
    int CarriageNumber,
    ClassType ClassType);