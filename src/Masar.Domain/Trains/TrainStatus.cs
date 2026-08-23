using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Trains
{
    public enum TrainStatus : byte
    {
        Active = 0,
        Inactive = 1,
        Maintenance = 2,
        Cancelled = 3
    }
}
