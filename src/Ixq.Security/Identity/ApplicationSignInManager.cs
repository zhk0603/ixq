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

namespace Ixq.Security.Identity
{
    public class ApplicationSignInManager<TUser>
        where TUser : class, IUser
    {
        public delegate TUser UserDelegate();

        public UserDelegate AA { get; set; }

        public static Lazy<SignInManager<TUser, string>> LazySignInManager;

        public static SignInManager<TUser, string> Instance => LazySignInManager.Value;

        public static TUser CurrentSystemUser
            => Instance.UserManager.FindById(Instance.AuthenticationManager.User.Identity.GetUserId());

        public static ClaimsPrincipal CurrentClaimsUser => Instance.AuthenticationManager.User;

        public static SignInStatus PasswordSignIn(string userName, string password, bool isPersistent,
            bool shouldLockout)
        {
            return Instance.PasswordSignIn(userName, password, isPersistent, shouldLockout);
        }

        public static Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent = false,
            bool shouldLockout = false)
        {
            return Instance.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);
        }

        public static SignInStatus TwoFactorSignIn(string provider, string code, bool isPersistent,
            bool rememberBrowser)
        {
            return Instance.TwoFactorSignIn(provider, code, isPersistent, rememberBrowser);
        }

        public static Task<SignInStatus> TwoFactorSignInAsync(string provider, string code, bool isPersistent,
            bool rememberBrowser)
        {
            return Instance.TwoFactorSignInAsync(provider,code,isPersistent,rememberBrowser);
        }

        public static void SignOut(params string[] authenticationTypes)
        {
            Instance.AuthenticationManager.SignOut(authenticationTypes);
        }

        public static void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        {
            Instance.AuthenticationManager.SignOut(properties, authenticationTypes);
        }
    }
}
