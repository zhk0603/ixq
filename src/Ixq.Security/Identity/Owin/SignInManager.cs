using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Ixq.Security.Identity.Owin
{
    public class AppSignInManager<TUser, TKey> : SignInManager<TUser, TKey>
        where TUser : class, IUser<TKey>, Ixq.Core.Security.IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        public AppSignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
        }

        public override async Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
        {
            await base.SignInAsync(user, isPersistent, rememberBrowser);
            OnSignInComplete(null);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var signInStatus = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            if (signInStatus == SignInStatus.Success)
            {
                OnSignInComplete(null);
            }

            return signInStatus;
        }

        public new virtual async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var signInStatus = await base.ExternalSignInAsync(loginInfo, isPersistent);
            if (signInStatus == SignInStatus.Success)
            {
                OnSignInComplete(null);
            }

            return signInStatus;
        }


        protected virtual void OnSignInComplete(TUser user)
        {
            if (user is IUserSpecification signInUser)
            {
                signInUser.LastSignInDate = DateTime.Now;
                signInUser.OnSignInComplete();
            }
            UserManager.Update(user);
        }

        protected virtual void OnSignOutComplete(TUser user)
        {
            if (user is IUserSpecification signOutUser)
            {
                signOutUser.LastSignOutDate = DateTime.Now;
                signOutUser.OnSignOutComplete();
            }
            UserManager.Update(user);
        }
    }
}
