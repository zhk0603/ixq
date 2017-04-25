using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Security.Identity;
using Ixq.Demo.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace Ixq.Demo.Domain
{
    public class ApplicationSignInManager : ApplicationSignInManagerBase<ApplicationUser>
    {
        public ApplicationSignInManager(UserManager<ApplicationUser, string> userManager, IAuthenticationManager authenticationManager) : base(userManager, authenticationManager)
        {
        }
    }
}
