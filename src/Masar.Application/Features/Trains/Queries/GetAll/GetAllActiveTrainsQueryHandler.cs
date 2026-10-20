using Masar.Application.Common.Interfaces;
using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Trains;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Masar.Application.Features.Trains.Queries.GetAll
{
    public class GetAllActiveTrainsQueryHandler(IAppDbContext context) : IRequestHandler<GetAllActiveTrainsQuery, Result<List<TrainSummaryDto>>>
    {
        private readonly IAppDbContext _context = context;
        public async Task<Result<List<TrainSummaryDto>>> Handle(GetAllActiveTrainsQuery query, CancellationToken cancellationToken)
        {
            var trains = await _context.Trains.AsNoTracking()
                .Where(x => x.Status == TrainStatus.Active)
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
