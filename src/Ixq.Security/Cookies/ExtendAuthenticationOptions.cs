using System;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;

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
            get => _cookieName;
            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");
                _cookieName = value;
            }
        }

        public SystemClock SystemClock { get; set; }

        public ISecureDataFormat<AuthenticationTicket> TicketDataFormat { get; set; }
        public ICookieManager CookieManager { get; set; }

        public Action<UserSignInContext<TUser, TKey>> OnUserSignIn { private get; set; }
        public Action<UserSignOutContext<TUser, TKey>> OnUserSignOut { private set; get; }

        public virtual void UserSignIn(UserSignInContext<TUser, TKey> context)
        {
            OnUserSignIn?.Invoke(context);
        }

        public virtual void UserSignOut(UserSignOutContext<TUser, TKey> context)
        {
            OnUserSignOut?.Invoke(context);
        }
    }
}