using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public class AppUserValidator<TUser> : UserValidator<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public AppUserValidator(UserManager<TUser, long> manager) : base(manager)
        {
        }
    }
}