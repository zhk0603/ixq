using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Security;
using Ixq.Security.Identity;
using Ixq.Demo.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using IUser = Ixq.Security.Identity.IUser;

namespace Ixq.Demo.Domain
{
    public class ApplicationSignInManager : SignInManager<ApplicationUser, string>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser, string> userManager,
            IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(ApplicationUser user)
        {
            return user.GenerateUserIdentityAsync((ApplicationUserManager) UserManager);
        }

        public static ApplicationSignInManager Create(IdentityFactoryOptions<ApplicationSignInManager> options,
            IOwinContext context)
        {
            var signInManager = new ApplicationSignInManager(context.GetUserManager<ApplicationUserManager>(),
                context.Authentication);
            ApplicationSignInManager<ApplicationUser>.LazySignInManager =
                new Lazy<SignInManager<ApplicationUser, string>>(() => signInManager);

            return signInManager;
        }

        public static SignInManager<IUser, string> Create1()
        {
            throw new NotImplementedException();
        }
    }
}
