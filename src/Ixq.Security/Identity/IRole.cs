using Ixq.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Security.Identity
{
    public interface IRole : Microsoft.AspNet.Identity.IRole, IEntity<string>
    {
        new string Id { get; set; }
    }
}
