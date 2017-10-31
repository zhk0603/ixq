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

namespace Ixq.Security.Owin
{
    public class AppSignInManager<TUser> : SignInManager<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public AppSignInManager(UserManager<TUser, long> userManager, IAuthenticationManager authenticationManager) :
            base(userManager, authenticationManager)
        {
        }
    }
}
