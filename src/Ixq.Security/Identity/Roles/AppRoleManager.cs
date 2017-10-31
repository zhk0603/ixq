using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public class AppRoleManager<TRole> : RoleManager<TRole, long>
        where TRole : class, IRole<long>, Ixq.Core.Security.IRole<long>
    {
        public AppRoleManager(IRoleStore<TRole, long> store) : base(store)
        {
        }
    }
}
