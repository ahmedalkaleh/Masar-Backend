using System;
using System.Collections.Generic;
using System.Text;

namespace Masar.Domain.Common
{
    public abstract class AuditableEntity : Entity

    {


        public AuditableEntity() { }
        public AuditableEntity(Guid id): base(id) { }

        public DateTimeOffset CreatedAt { get; set; }

        public string? CreatedBy { get; set; }
        public DateTimeOffset? LastModifiedAt { get; set; }

        public string? LastModifiedBy { get; set; }
    }
}
