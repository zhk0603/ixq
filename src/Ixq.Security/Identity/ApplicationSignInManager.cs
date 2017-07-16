using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.Owin;

namespace Ixq.Security.Identity
{
    public class ApplicationSignInManager<TUser>
        where TUser : class, IUser
    {
        private static SignInManager<TUser, string> _manager;

        public static TUser CurrentSystemUser
            => _manager.UserManager.FindById(_manager.AuthenticationManager.User.Identity.GetUserId());

        public static ClaimsPrincipal CurrentClaimsUser => _manager.AuthenticationManager.User;

        public static SignInManager<TUser, string> Create<TManager>(IdentityFactoryOptions<SignInManager<TUser, string>> options, IOwinContext context)
            where TManager : UserManager<TUser, string>
        {
            var signInManager = new SignInManager<TUser, string>(context.GetUserManager<TManager>(),
                context.Authentication);
            _manager = signInManager;
            return signInManager;
        }

        public static SignInStatus PasswordSignIn(string userName, string password, bool isPersistent,
            bool shouldLockout)
        {
            return _manager.PasswordSignIn(userName, password, isPersistent, shouldLockout);
        }

        public static Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent = false,
            bool shouldLockout = false)
        {
            return _manager.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }

        public static SignInStatus TwoFactorSignIn(string provider, string code, bool isPersistent,
            bool rememberBrowser)
        {
            return _manager.TwoFactorSignIn(provider, code, isPersistent, rememberBrowser);
        }

        public static Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent,
            bool rememberBrowser)
        {
            return _manager.TwoFactorSignInAsync(provider,code,isPersistent,rememberBrowser);
        }

        public static void SignOut(params string[] authenticationTypes)
        {
            _manager.AuthenticationManager.SignOut(authenticationTypes);
        }

        public static void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            _manager.AuthenticationManager.SignOut(properties, authenticationTypes);
        }
    }
}
