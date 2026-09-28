using Masar.Application.Features.Stations.Dtos;
using Masar.Domain.Common.Results;
using Masar.Domain.Stations;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Features.Stations.Commands.CreateStation
{
    /// <summary>
    /// Request used to create a new railway station.
    /// </summary>
    public sealed record CreateStationCommand : IRequest<Result<StationDto>>
    {
        /// <summary>
        /// The Arabic name of the station.
        /// Maximum length: 100 characters.
        /// </summary>
        public string NameAr { get; init; } = null!;

        /// <summary>
        /// The English name of the station.
        /// Maximum length: 100 characters.
        /// </summary>
        public string NameEn { get; init; } = null!;

        /// <summary>
        /// The type of the station.
        /// 0 = Main, 1 = Sub.
        /// </summary>
        public StationType Type { get; init; }

        /// <summary>
        /// The latitude of the station.
        /// Must be between -90 and 90.
        /// </summary>
        public decimal Latitude { get; init; }

        /// <summary>
        /// The longitude of the station.
        /// Must be between -180 and 180.
        /// </summary>
        public decimal Longitude { get; init; }

        /// <summary>
        /// The governorate where the station is located.
        /// Maximum length: 100 characters.
        /// </summary>
        public string Governorate { get; init; } = null!;

        /// <summary>
        /// Customs processing delay in minutes.
        /// Must be greater than or equal to 0.
        /// </summary>
        public int CustomsDelayMinutes { get; init; }
    }
}
