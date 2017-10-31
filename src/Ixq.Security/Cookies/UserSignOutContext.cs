using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Provider;

namespace Ixq.Security.Cookies
{
    public class UserSignOutContext<TUser> : UserSignOutContext<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public UserSignOutContext(IOwinContext context, 
            ExtendAuthenticationOptions<TUser, long> options, 
            TUser user
            ) : base(context, options, user)
        {
        }
    }


    public class UserSignOutContext<TUser,TKey> : BaseContext<ExtendAuthenticationOptions<TUser, TKey>>
        where TUser : class, IUser<TKey>, Core.Security.IUser<TKey>

    {
        public UserSignOutContext(IOwinContext context,
            ExtendAuthenticationOptions<TUser, TKey> options,
            TUser user
            ) : base(context, options)
        {
            this.User = user;
        }

        public TUser User { get; set; }
    }
}
