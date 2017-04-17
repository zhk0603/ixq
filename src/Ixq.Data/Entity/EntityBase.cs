using System;
using Ixq.Core.Entity;

namespace Ixq.Data.Entity
{
    public abstract class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}