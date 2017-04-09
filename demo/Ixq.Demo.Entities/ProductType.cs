using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Demo.Entities
{
    public class ProductType : EntityBase
    {
        public string Name { get; set; }
        public virtual ProductType ParentType { get; set; }
    }
}
