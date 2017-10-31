using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppIdentityUserClaim : IdentityUserClaim<long>
    {
        public new long Id { get; set; }
    }
}