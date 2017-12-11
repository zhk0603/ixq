using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public interface IAppUserStore<TUser> : IUserStore<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
    }
}