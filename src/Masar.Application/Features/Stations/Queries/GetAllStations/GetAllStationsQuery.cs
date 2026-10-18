using System;
using Masar.Application.Features.Stations.Dtos;
using MediatR;

namespace Masar.Application.Features.Stations.Queries.GetAllStations;

/// <summary>
/// Retrieves all active railway stations.
/// </summary>
public sealed record GetAllStationsQuery
    : IRequest<List<StationDto>>;