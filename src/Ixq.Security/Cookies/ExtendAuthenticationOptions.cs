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
    public class ExtendAuthenticationOptions<TUser> : ExtendAuthenticationOptions<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
    }


    public class ExtendAuthenticationOptions<TUser, TKey> : AuthenticationOptions
        where TUser : class, IUser<TKey>, Core.Security.IUser<TKey>
    {
        private string _cookieName;
        public ExtendAuthenticationOptions() : this(DefaultAuthenticationTypes.ApplicationCookie)
        {
        }

        public ExtendAuthenticationOptions(string authenticationType) : base(authenticationType)
        {
            SystemClock = new SystemClock();
        }

        public string CookieName
        {
            get { return _cookieName; }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                _cookieName = value;
            }
        }

        public SystemClock SystemClock { get; set; }

        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }
        public ICookieManager CookieManager { get; set; }

        public Action<UserSignInContext<TUser, TKey>> OnUserSignIn { private get; set; }

        public virtual void UserSignIn(UserSignInContext<TUser, TKey> context)
        {
            OnUserSignIn?.Invoke(context);
        }
    }
}
