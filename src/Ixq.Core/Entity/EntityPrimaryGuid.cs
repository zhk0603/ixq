using System;

namespace Ixq.Core.Entity
{
    public class EntityPrimaryGuid : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}