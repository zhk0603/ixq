using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public class AppUserManager<TUser> : UserManager<TUser,long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public AppUserManager(IAppUserStore<TUser> store) : base(store)
        {
        }
    }
}
