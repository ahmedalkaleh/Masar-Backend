using Masar.Application.Features.Stations.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.CreateStation
{
    public sealed record CreateStationCommand(
    string NameAr,
    string NameEn,
    StationType Type,
    decimal Latitude,
    decimal Longitude,
    string Governorate,
    int CustomsDelayMinutes) : IRequest<Result<StationDto>>
    { }
    
}
