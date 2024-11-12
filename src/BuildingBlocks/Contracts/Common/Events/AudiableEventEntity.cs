using Contracts.Domains.Interfaces;

namespace Contracts.Common.Events
{
    public abstract class AudiableEventEntity<T> : EventEntity<T>, IAuditable
    {
        public DateTimeOffset CreatedDate { get; set; }
        public DateTimeOffset? LastModifiedDate { get; set; }
    }
}
