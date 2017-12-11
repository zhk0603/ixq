using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;

namespace Ixq.Security.Owin
{
    public static class SecurityStampValidator
    {
        public static Func<CookieValidateIdentityContext, Task> OnValidateIdentity<TManager, TUser>(
            TimeSpan validateInterval, Func<TManager, TUser, Task<ClaimsIdentity>> regenerateIdentity)
            where TManager : UserManager<TUser, long>
            where TUser : class, IUser<long>, Core.Security.IUser<long>
        {
            return Microsoft.AspNet.Identity.Owin.SecurityStampValidator.OnValidateIdentity(validateInterval,
                regenerateIdentity, id => id.GetUserId<long>());
        }
    }
}