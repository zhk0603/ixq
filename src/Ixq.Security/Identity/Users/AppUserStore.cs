using System.Data.Entity;
using Ixq.Core.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppUserStore<TUser> :
        UserStore<TUser, AppIdentityRole, long, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>,
        IAppUserStore<TUser>
        where TUser : IdentityUser<long, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>,
        IUser<long>
    {
        public AppUserStore(DbContext context) : base(context)
        {
        }
    }
}