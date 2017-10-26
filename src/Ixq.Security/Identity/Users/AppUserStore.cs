using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppUserStore<TUser> :
        UserStore<TUser, AppIdentityRole, long, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>,
        IAppUserStore<TUser>
        where TUser : IdentityUser<long, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>,
        Ixq.Core.Security.IUser<long>
    {
        public AppUserStore(DbContext context) : base(context)
        {
        }
    }
}
