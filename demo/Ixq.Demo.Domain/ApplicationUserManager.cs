using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;

namespace Ixq.Demo.Domain
{
    public class ApplicationUserManager : ApplicationUserManagerBase<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}