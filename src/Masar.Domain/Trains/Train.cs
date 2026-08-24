using Masar.Domain.Carriages;
using Masar.Domain.Common;
using Masar.Domain.Common.Results;
using Masar.Domain.Persons;
using Masar.Domain.Stations;
using Masar.Domain.Trips;
using Masar.Domain.Users;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Masar.Domain.Trains;

public partial class Train : AuditableEntity
{
    public string Code { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public string TrainType { get; private set; } = null!;

    public int MaxSpeedKmh { get; private set; }

    public TrainStatus Status { get; private set; }

    public bool IsDelete { get; private set; }

    public Guid? CurrentStationId { get; private set; }

    public virtual ICollection<Carriage> Carriages { get; private set; } = new List<Carriage>();

    public virtual ICollection<Trip> Trips { get; private set; } = new List<Trip>();

    public virtual Station? Station { get; private set; } = null!;

    private Train() { }


    private Train(
    Guid id,
    string code,
    string name,
    string trainType,
    int maxSpeedKmh,
    Guid currentStationId)
        :base(id)
    {
        Code = code;
        Name = name;
        TrainType = trainType;
        MaxSpeedKmh = maxSpeedKmh;
        CurrentStationId = currentStationId;

        Status = TrainStatus.Active;
        IsDelete = false;
    }

    public static Result<Train> Create(
    Guid id,
    string code,
    string name,
    string trainType,
    int maxSpeedKmh,
    Guid currentStationId)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return TrainErrors.CodeRequired;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return TrainErrors.NameRequired;
        }
      
        if (string.IsNullOrWhiteSpace(trainType))
        {
            return TrainErrors.TrainTypeRequired;
        }

        if (maxSpeedKmh <= 0)
        {
            return TrainErrors.InvalidMaxSpeedKmh;
        }

        return new Train(id, code, name, trainType, maxSpeedKmh,currentStationId);
    }


    public Result<Updated> Update(string code,string name,string trainType,int maxSpeedKmh)
    {
        if (string.IsNullOrWhiteSpace(code))
        {
            return TrainErrors.CodeRequired;
        }

        if (string.IsNullOrWhiteSpace(name))
        {
            return TrainErrors.NameRequired;
        }

        if (string.IsNullOrWhiteSpace(trainType))
        {
            return TrainErrors.TrainTypeRequired;
        }

        if (maxSpeedKmh <= 0)
        {
            return TrainErrors.InvalidMaxSpeedKmh;
        }

        Code = code;
        Name = name;
        TrainType = trainType;
        MaxSpeedKmh = maxSpeedKmh;
        return Result.Updated;
    }


}
