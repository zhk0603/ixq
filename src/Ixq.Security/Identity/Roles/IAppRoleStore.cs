using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public interface IAppRoleStore<TRole> : IRoleStore<TRole, long>
        where TRole : IRole<long>, Core.Security.IRole<long>
    {
    }
}