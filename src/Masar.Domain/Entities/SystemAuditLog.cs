using System;
using System.Collections.Generic;

namespace Masar.Infrastructure;

public partial class SystemAuditLog
{
    public long LogId { get; set; }

    public int? UserId { get; set; }

    public string Action { get; set; } = null!;

    public string EntityName { get; set; } = null!;

    public string? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? Ipaddress { get; set; }

    public DateTime Timestamp { get; set; }
}
