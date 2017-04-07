using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Data.Entity
{
    public abstract class EntityBase : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
