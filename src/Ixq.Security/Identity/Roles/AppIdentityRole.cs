using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppIdentityRole : IdentityRole<long, AppIdentityUserRole>, Ixq.Core.Security.IRole<long>
    {
        public AppIdentityRole() { }
        public AppIdentityRole(string name)
        {
            Name = name;
        }
    }
}
