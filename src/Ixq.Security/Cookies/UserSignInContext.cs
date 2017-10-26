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
    public class UserSignInContext<TUser> : BaseContext<AppAuthenticationOptions<TUser>>
        where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
    {
        public UserSignInContext(IOwinContext context,
            AppAuthenticationOptions<TUser> options,
            string authenticationType,
            TUser user)
            : base(context, options)
        {
            AuthenticationType = authenticationType;
            User = user;
        }

        public string AuthenticationType { get; private set; }
        public TUser User { get; set; }
    }
}
