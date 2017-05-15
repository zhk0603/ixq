using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public abstract class IdentityUserBase : IdentityUser, IEntity<string>
    {
        protected IdentityUserBase() { }
        protected IdentityUserBase(string userName) : base(userName) { }

    }
}