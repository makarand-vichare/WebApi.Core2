using System;

namespace WebApi.Core.EntityModels.Core
{
    [Serializable]
    public abstract class AuditableEntity : IdentityColumnEntity
    {
        public DateTime UpdatedOn { get; set; }
        public long UpdatedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
