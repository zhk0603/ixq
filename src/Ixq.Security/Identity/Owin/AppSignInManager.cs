using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ixq.Core.Logging;

namespace Ixq.Security.Identity.Owin
{
    public class AppSignInManager<TUser, TKey> : SignInManager<TUser, TKey>
        where TUser : class, IUser<TKey>, Ixq.Core.Security.IUser<TKey>
        where TKey : IEquatable<TKey>
    {
        private readonly ILogger _logger;
        public AppSignInManager(UserManager<TUser, TKey> userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
            _logger = Ixq.Core.Logging.LogManager.GetLogger(GetType());
        }

        protected virtual ILogger Logger => _logger;

        public override async Task SignInAsync(TUser user, bool isPersistent, bool rememberBrowser)
        {
            await base.SignInAsync(user, isPersistent, rememberBrowser);
            OnSignInComplete(user);
        }

        public override async Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            var signInStatus = await base.PasswordSignInAsync(userName, password, isPersistent, shouldLockout);

            if (signInStatus == SignInStatus.Success)
            {
                var user = await UserManager.FindByNameAsync(userName);
                OnSignInComplete(user);
            }
            return signInStatus;
        }

        public new virtual async Task<SignInStatus> ExternalSignInAsync(ExternalLoginInfo loginInfo, bool isPersistent)
        {
            var signInStatus = await base.ExternalSignInAsync(loginInfo, isPersistent);
            if (signInStatus == SignInStatus.Success)
            {
                var user = await UserManager.FindByNameAsync(loginInfo.DefaultUserName);
                OnSignInComplete(user);
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
            var result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                Logger?.Error("更新用户出错：" + string.Join("\r\n", result.Errors));
            }
        }

        protected virtual void OnSignOutComplete(TUser user)
        {
            if (user is IUserSpecification signOutUser)
            {
                signOutUser.LastSignOutDate = DateTime.Now;
                signOutUser.OnSignOutComplete();
            }
            var result = UserManager.Update(user);
            if (!result.Succeeded)
            {
                Logger?.Error("更新用户出错：" + string.Join("\r\n", result.Errors));
            }
        }
    }
}
