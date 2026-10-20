using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Stations.Dtos;
using Masar.Application.Features.Stations.Mappers;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Features.Stations.Queries.GetAllStations;

public sealed class GetAllStationsQueryHandler(
    IAppDbContext context)
    : IRequestHandler<GetAllStationsQuery, Result<List<StationSummaryDto>>>
{
    private readonly IAppDbContext _context = context;

    public async Task<Result<List<StationSummaryDto>>> Handle(
        GetAllStationsQuery request,
        CancellationToken cancellationToken)
    {
        var stations = await _context.Stations
            .AsNoTracking()
            .Select(x => 
            new StationSummaryDto
            {
                 StationID = x.Id,
                 NameAr = x.NameAr,
                 NameEn = x.NameEn,
                 Governorate = x.Governorate
            })
            .OrderBy(station => station.Governorate)
            .ThenBy(station => station.NameAr)
            .ToListAsync(cancellationToken);

        return stations;
    }
}