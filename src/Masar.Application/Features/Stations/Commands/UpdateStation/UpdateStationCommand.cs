using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.UpdateStation
{
    public sealed record UpdateStationCommand(
    Guid StationID,
    string NameAr,
    string NameEn,
    StationType Type,
    decimal Latitude,
    decimal Longitude,
    string Governorate,
    int CustomsDelayMinutes) : IRequest<Result<Updated>>
    {
    }
}
