using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;

namespace Ixq.Security.Cookies
{
    public class AppAuthenticationOptions<TUser> : AuthenticationOptions
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public AppAuthenticationOptions() : base(DefaultAuthenticationTypes.ApplicationCookie)
        {
        }

        public AppAuthenticationOptions(string authenticationType) : base(authenticationType)
        {
        }
        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }
        public ICookieManager CookieManager { get; set; }

        public Action<UserSignInContext<TUser>> OnUserSignIn { private get; set; }

        public virtual void UserSignIn(UserSignInContext<TUser> context)
        {
            OnUserSignIn?.Invoke(context);
        }
    }
}
