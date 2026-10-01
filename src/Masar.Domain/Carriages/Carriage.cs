using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Seats;
using Masar.Domain.Trains;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Carriages;

public partial class Carriage : AuditableEntity
{
    public Guid TrainId { get; private set; }

    public int CarriageNumber { get; private set; }

    public ClassType ClassType { get; private set; }

    public short TotalSeats { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual ICollection<Seat> Seats { get; private set; } = new List<Seat>();

    public virtual Train Train { get; private set; } = null!;

    private Carriage() { }


    private Carriage(
    Guid id,
    Guid trainId,
    int carriageNumber,
    ClassType classType,
    short totalSeats)
        :base(id)
    {
        TrainId = trainId;
        CarriageNumber = carriageNumber;
        ClassType = classType;
        TotalSeats = totalSeats;

        IsDelete = false;
    }

    public static Result<Carriage> Create(
    Guid id,
    Guid trainId,
    int carriageNumber,
    ClassType classType,
    short totalSeats)
    {
        if(trainId == Guid.Empty)
        {
            return CarriageErrors.TrainIdRequired;
        }

        if(carriageNumber < 0)
        {
            return CarriageErrors.InvalidCarriageNumber;
        }

        if (!Enum.IsDefined(typeof(ClassType), classType))
        {
            return CarriageErrors.InvalidClassType;
        }

        if (totalSeats <= 0)
        {
            return CarriageErrors.InvalidTotalSeats;
        }


        return new Carriage(id, trainId, carriageNumber, classType, totalSeats);

    }


    public Result<Updated> Update(Guid trainId,int carriageNumber,ClassType classType,short totalSeats)
    {
        if (trainId == Guid.Empty)
        {
            return CarriageErrors.TrainIdRequired;
        }

        if (carriageNumber < 0)
        {
            return CarriageErrors.InvalidCarriageNumber;
        }

        if (!Enum.IsDefined(typeof(ClassType), classType))
        {
            return CarriageErrors.InvalidClassType;
        }

        if (totalSeats <= 0)
        {
            return CarriageErrors.InvalidTotalSeats;
        }

        TrainId = trainId;
        CarriageNumber = carriageNumber;
        ClassType = classType;
        TotalSeats = totalSeats;

        return Result.Updated;
    }
}
