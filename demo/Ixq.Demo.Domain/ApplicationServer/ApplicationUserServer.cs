using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;

namespace Ixq.Demo.Domain.ApplicationServer
{
    public class ApplicationUserServer : ApplicationUserManagerBase<ApplicationUser>
    {
        public ApplicationUserServer(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}