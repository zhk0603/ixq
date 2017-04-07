using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    public class EntityPrimaryGuid : IEntity<Guid>
    {
        public Guid Id { get; set; }
    }
}
