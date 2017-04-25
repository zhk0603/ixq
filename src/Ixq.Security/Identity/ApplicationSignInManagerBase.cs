using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationSignInManagerBase<TUser> : SignInManager<TUser, string>, IScopeDependency
        where TUser : class, IUser<string>
    {
        protected ApplicationSignInManagerBase(UserManager<TUser, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}
