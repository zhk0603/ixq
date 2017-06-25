using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Ixq.Extensions.ObjectModel;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationSignInManagerBase<TUser> : SignInManager<TUser, string>,
        ISignInManager<TUser>, IScopeDependency
        where TUser : class, IUser<string>
    {
        private readonly SignInManager<TUser, string> _signInManager;

        protected ApplicationSignInManagerBase(UserManager<TUser, string> userManager,
            IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
            _signInManager = this;
        }

        public TUser CurrentUser => UserManager.FindById(AuthenticationManager.User.Identity.GetUserId());

        public void SignIn(TUser user, bool rememberBrowser)
        {
            _signInManager.SignIn(user, false, rememberBrowser);
        }

        public Task SignInAsync(TUser user, bool rememberBrowser)
        {
            return _signInManager.SignInAsync(user, false, rememberBrowser);
        }

        public void SignIn(string userId, bool rememberBrowser)
        {
            var user = UserManager.FindById(userId);
            _signInManager.SignIn(user, false, rememberBrowser);
        }

        public Task SignInAsync(string userId, bool rememberBrowser)
        {
            var user = UserManager.FindById(userId);
            return _signInManager.SignInAsync(user, false, rememberBrowser);
        }

        public void SignOut(params string[] authenticationTypes)
        {
            AuthenticationManager.SignOut(authenticationTypes);
        }

        public Core.Security.SignInStatus PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return (Core.Security.SignInStatus)Enum.Parse(typeof(Core.Security.SignInStatus), _signInManager.PasswordSignIn(userName, password, isPersistent, shouldLockout).ToString());
        }

        Task<Core.Security.SignInStatus> ISignInManager<TUser>.PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout)
        {
            return Task.FromResult(PasswordSignIn(userName, password, isPersistent, shouldLockout));
        }
    }
}
