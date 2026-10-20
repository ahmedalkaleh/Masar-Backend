using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Masar.Application.Features.Trains.Queries.GetAll
{
    public class GetAllTrainsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllTrainsQuery, Result<List<TrainSummaryDto>>>
    {
        private readonly IAppDbContext _context = context;

        public async Task<Result<List<TrainSummaryDto>>> Handle(GetAllTrainsQuery query, CancellationToken cancellationToken)
        {
            var trains = await _context.Trains.AsNoTracking()
                .Select(x =>
                new TrainSummaryDto
                {
                    TrainID = x.Id,
                    Code = x.Code,
                    Name = x.Name,
                    TrainType = x.TrainType,
                })
                .OrderBy(x => x.Name)
                .ToListAsync(cancellationToken);

            return trains;
        }

    }

}
