using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.AspNet.Identity.Owin;

namespace Ixq.Security.Cookies
{
    public class AppAuthenticationHandler<TManager, TUser> : AuthenticationHandler<AppAuthticationOptions<TUser>>
        where TManager : UserManager<TUser, long>
        where TUser : class, Microsoft.AspNet.Identity.IUser<long>, Ixq.Core.Security.IUser<long>
    {
        protected override Task InitializeCoreAsync()
        {
            if (Request.User != null && Request.User.Identity != null)
            {
                var appIdentity = new AppIdentity(Request.User.Identity);
                Request.User = new Ixq.Core.Security.AppPrincipal(new List<AppIdentity>
                {
                    appIdentity
                });
            }

            return base.InitializeCoreAsync();
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
