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

namespace Ixq.Demo.Domain
{
    public class ApplicationSignInManager : ApplicationSignInManager<ApplicationUser>
    {
    }
}
