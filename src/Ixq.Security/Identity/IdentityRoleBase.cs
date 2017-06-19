using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public abstract class IdentityRoleBase : IdentityRole, IRole, IEntity<string>
    {
        protected IdentityRoleBase() { }
        protected IdentityRoleBase(string name) : base(name) { }
    }
}