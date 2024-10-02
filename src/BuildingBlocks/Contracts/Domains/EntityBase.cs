using Contracts.Domains.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Domains
{
    public abstract class EntityBase<TKey> : IEntityBase<TKey>
    {
        public TKey Id { get; set; }
    }
}
