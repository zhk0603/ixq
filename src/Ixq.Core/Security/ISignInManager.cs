using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;
using Ixq.Extensions.ObjectModel;

namespace Ixq.Core.Security
{
    public interface ISignInManager<TUser> : IScopeDependency
        where TUser :class
    {
        TUser CurrentUser { get; }
        SignInStatus PasswordSignIn(string userName, string password, bool isPersistent, bool shouldLockout);
        Task<SignInStatus> PasswordSignInAsync(string userName, string password, bool isPersistent, bool shouldLockout);
        void SignIn(TUser user, bool rememberBrowser);
        Task SignInAsync(TUser user, bool rememberBrowser);
        void SignIn(string userId, bool rememberBrowser);
        Task SignInAsync(string userId, bool rememberBrowser);
        void SignOut(params string[] authenticationTypes);
    }
}
