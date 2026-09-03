using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Tickets;
using Masar.Domain.Trains;
using System;
using System.Collections.Generic;

namespace Masar.Domain.Seats;

public partial class Seat : AuditableEntity
{
    public Guid CarriageId { get; private set; }

    public string SeatNumber { get; private set; } = null!;

    public string RowNumber { get; private set; } = null!;

    public byte ColumnNumber { get; private set; } 

    public SeatType SeatType { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsDelete { get; private set; }

    public virtual Carriage Carriage { get; private set; } = null!;

    public virtual ICollection<Ticket> Tickets { get; private set; } = new List<Ticket>();

    private Seat() { }


    private Seat(
    Guid id,
    Guid carriageId,
    string rowNumber,
    byte columnNumber,
    SeatType seatType)
        :base(id)
    {
        CarriageId = carriageId;       
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        SeatType = seatType;

        SeatNumber = rowNumber.Trim() + ColumnNumber.ToString();
        IsActive = true;
        IsDelete = false;
    }

    public static Result<Seat> Create(
    Guid id,
    Guid carriageId,
    string rowNumber,
    byte columnNumber,
    SeatType seatType)
    {
        var errorsList = new List<Error>();

        if (carriageId == Guid.Empty)
        {
            errorsList.Add(SeatErrors.CarriageIdRequired);
        }

        if(string.IsNullOrWhiteSpace(rowNumber))
        {
            errorsList.Add(SeatErrors.RowNumberRequired);
        }

        if(rowNumber.Length > 2)
        {
            errorsList.Add(SeatErrors.RowNumberTooLong);
        }

        if(columnNumber < 1 || columnNumber > 6)
        {
            errorsList.Add(SeatErrors.InvalidColumnNumber);
        }

        if(!Enum.IsDefined(typeof(SeatType), seatType))
        {
            errorsList.Add(SeatErrors.InvalidSeatType);
        }

        if (errorsList.Count > 0)
        {
            return errorsList;
        }


        return new Seat(id, carriageId, rowNumber, columnNumber, seatType);
    }


    public Result<Updated> Update(string rowNumber,byte columnNumber,
         SeatType seatType,bool isActive)
    {

        var errorsList = new List<Error>();

        if (string.IsNullOrWhiteSpace(rowNumber))
        {
            errorsList.Add(SeatErrors.RowNumberRequired);
        }

        if (rowNumber.Length > 2)
        {
            errorsList.Add(SeatErrors.RowNumberTooLong);
        }

        if (columnNumber < 1 || columnNumber > 6)
        {
            errorsList.Add(SeatErrors.InvalidColumnNumber);
        }

        if ((int)seatType < 0 || (int)seatType > 1)
        {
            errorsList.Add(SeatErrors.InvalidSeatType);
        }

        if (errorsList.Count > 0)
        {
            return errorsList;
        }


        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        SeatType = seatType;
        IsActive = isActive;

        SeatNumber = rowNumber.Trim() + ColumnNumber.ToString();

        return Result.Updated;
    }

    
}
