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
    public class UserSignInContext<TUser> : UserSignInContext<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public UserSignInContext(IOwinContext context,
            ExtendAuthenticationOptions<TUser> options,
            string authenticationType,
            TUser user)
            : base(context, options, authenticationType, user)
        {
        }
    }

    public class UserSignInContext<TUser, TKey> : BaseContext<ExtendAuthenticationOptions<TUser, TKey>>
        where TUser : class, IUser<TKey>, Core.Security.IUser<TKey>
    {
        public UserSignInContext(IOwinContext context,
            ExtendAuthenticationOptions<TUser, TKey> options,
            string authenticationType,
            TUser user) : base(context, options)
        {
            AuthenticationType = authenticationType;
            User = user;
        }

        public string AuthenticationType { get; private set; }
        public TUser User { get; set; }

    }
}
