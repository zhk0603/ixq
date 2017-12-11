using System.Data.Entity;
using Ixq.Core.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppRoleStore<TRole> : RoleStore<TRole, long, AppIdentityUserRole>, IAppRoleStore<TRole>
        where TRole : IdentityRole<long, AppIdentityUserRole>, IRole<long>, new()
    {
        public AppRoleStore(DbContext context) : base(context)
        {
        }
    }
}