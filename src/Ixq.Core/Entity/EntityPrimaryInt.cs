using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    class EntityPrimaryInt:IEntity<int>
    {
        public int Id { get; set; }
    }
}
