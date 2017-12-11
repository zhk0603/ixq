using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

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