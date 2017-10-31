using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public interface IAppRoleStore<TRole> : IRoleStore<TRole, long>
        where TRole : IRole<long>, Ixq.Core.Security.IRole<long>
    {
    }
}
