using Ixq.Core.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppIdentityRole : IdentityRole<long, AppIdentityUserRole>, IRole<long>
    {
        public AppIdentityRole()
        {
        }

        public AppIdentityRole(string name)
        {
            Name = name;
        }
    }
}