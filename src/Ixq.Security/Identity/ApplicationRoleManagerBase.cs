using Ixq.Core.DependencyInjection;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationRoleManagerBase<TRole> : RoleManager<TRole>, IScopeDependency
        where TRole : class, IRole<string>
    {
        protected ApplicationRoleManagerBase(IRoleStore<TRole, string> store) : base(store)
        {
        }
    }
}