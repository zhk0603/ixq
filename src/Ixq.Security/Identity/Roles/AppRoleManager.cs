using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public class AppRoleManager<TRole> : RoleManager<TRole, long>
        where TRole : class, IRole<long>, Core.Security.IRole<long>
    {
        public AppRoleManager(IRoleStore<TRole, long> store) : base(store)
        {
        }
    }
}