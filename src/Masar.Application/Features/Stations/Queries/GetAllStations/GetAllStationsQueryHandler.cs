using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Mappers;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Features.Stations.Queries.GetAllStations;

public sealed class GetAllStationsQueryHandler(
    IAppDbContext context)
    : IRequestHandler<GetAllStationsQuery, List<StationDto>>
{
    private readonly IAppDbContext _context = context;

    public async Task<List<StationDto>> Handle(
        GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        var stations = await _context.Stations
            .AsNoTracking()
            .Where(station => !station.IsDelete)
            .OrderBy(station => station.Governorate)
            .ThenBy(station => station.NameAr)
            .ToListAsync(cancellationToken);

        return stations.ToDto();
    }
}