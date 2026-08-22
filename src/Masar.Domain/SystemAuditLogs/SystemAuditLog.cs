using Masar.Domain.Common;
using System;
using System.Collections.Generic;

namespace Masar.Domain.SystemAuditLogs;

public partial class SystemAuditLog : AuditableEntity
{
    public Guid? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public string? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? Ipaddress { get; set; }

    public DateTime Timestamp { get; set; }

    private SystemAuditLog() { }


    private SystemAuditLog(
    Guid id,
    Guid? userId,
    string action,
    string entityName,
    string? entityId,
    string? oldValues,
    string? newValues,
    string? ipaddress,
    DateTime timestamp)
        :base(id)
    {
        UserId = userId;
        Action = action;
        EntityName = entityName;
        EntityId = entityId;
        OldValues = oldValues;
        NewValues = newValues;
        Ipaddress = ipaddress;
        Timestamp = timestamp;
    }
}
