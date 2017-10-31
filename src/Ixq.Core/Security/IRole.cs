using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Security
{
    public interface IRole<TKey> : IEntity<TKey>
    {
        string Name { get; set; }
    }
}
