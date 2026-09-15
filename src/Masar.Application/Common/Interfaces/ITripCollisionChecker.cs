using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Application.Common.Interfaces
{
    public record SegmentOccupancy(
        Guid FirstStationId,
        Guid SecondStationId,
        DateTime EntryTime,
        DateTime ExitTime
    );
    public interface ITripCollisionChecker
    {
        Task<bool> HasCollisionAsync(
        List<SegmentOccupancy> newTripOccupancies,
        DateTime overallStartTime,
        DateTime overallEndTime,
        CancellationToken ct);
    }
}
