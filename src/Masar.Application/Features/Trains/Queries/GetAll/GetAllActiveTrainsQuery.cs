using Masar.Application.Features.Trains.Dtos;
using Masar.Domain.Common.Results;
using MediatR;

namespace Masar.Application.Features.Trains.Queries.GetAll
{
    public sealed record GetAllActiveTrainsQuery : IRequest<Result<List<TrainSummaryDto>>>;


}
