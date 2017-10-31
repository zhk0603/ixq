using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Ixq.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.AspNet.Identity.Owin;

namespace Ixq.Security.Cookies
{
    public class ExtendAuthenticationHandler<TManager, TUser> : AuthenticationHandler<ExtendAuthenticationOptions<TUser>>
        where TManager : UserManager<TUser, long>
        where TUser : class, Microsoft.AspNet.Identity.IUser<long>, Ixq.Core.Security.IUser<long>
    {
        private const string LoginIp = "LoginIp";
        private const string LoginTime = "LoginTime";

        protected override Task InitializeCoreAsync()
        {
            if (Request.User?.Identity is ClaimsIdentity)
            {
                var appPrincipal = new AppPrincipal(Request.User.Identity);
                var ticket = GetAuthenticationTicket();
                if (ticket?.Identity != null)
                {
                    var userWrap = new CurrentUserWrap
                    {
                        LoginIp = ticket.Properties.Dictionary[LoginIp],
                        LoginTime = Convert.ToDateTime(ticket.Properties.Dictionary[LoginTime]),
                        ClaimsPrincipal = appPrincipal
                    };
                    appPrincipal.Identity.UserInfo = userWrap;
                }
                Request.User = appPrincipal;
                System.Threading.Thread.CurrentPrincipal = appPrincipal;
            }
            return Task.FromResult<object>(null);
        }

        protected virtual AuthenticationTicket GetAuthenticationTicket()
        {
            string cookie = Options.CookieManager.GetRequestCookie(Context, Options.CookieName);
            if (string.IsNullOrWhiteSpace(cookie))
            {
                return null;
            }
            var ticket = Options.TicketDataFormat.Unprotect(cookie);
            if (ticket == null)
            {
                return null;
            }
            return new AuthenticationTicket(ticket.Identity, ticket.Properties);
        }

        protected override Task<AuthenticationTicket> AuthenticateCoreAsync()
        {
            return Task.FromResult<AuthenticationTicket>(null);
        }

        protected override async Task ApplyResponseGrantAsync()
        {
            AuthenticationResponseGrant signin = Helper.LookupSignIn(Options.AuthenticationType);
            bool shouldSignin = signin != null;
            AuthenticationResponseRevoke signout =
                Helper.LookupSignOut(Options.AuthenticationType, Options.AuthenticationMode);
            bool shouldSignout = signout != null;
            var manager = Context.GetUserManager<TManager>();
            if (shouldSignin)
            {
                var user = await manager.FindByIdAsync(signin.Identity.GetUserId<long>());
                ApplySignInIUserSpecification(user);

                var signInContext = new UserSignInContext<TUser>(Context,
                    Options,
                    Options.AuthenticationType,
                    user);
                Options.UserSignIn(signInContext);
                await manager.UpdateAsync(user);

                signin.Properties.Dictionary[LoginTime] = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                signin.Properties.Dictionary[LoginIp] = NetHelper.CurrentIp;

            }
            else if (shouldSignout)
            {
                var userId = Context.Authentication.User.Identity.GetUserId<long>();
                var user = await manager.FindByIdAsync(userId);
                ApplySignOutIUserSpecification(user);
                await manager.UpdateAsync(user);
            }
        }

        protected virtual void ApplySignInIUserSpecification(TUser user)
        {
            if (user is IUserSpecification signInUser)
            {
                signInUser.LastSignInDate = DateTime.Now;
                signInUser.OnSignInComplete();
            }
        }

        protected virtual void ApplySignOutIUserSpecification(TUser user)
        {
            if (user is IUserSpecification signInUser)
            {
                signInUser.LastSignOutDate = DateTime.Now;
                signInUser.OnSignOutComplete();
            }
        }
    }
}
