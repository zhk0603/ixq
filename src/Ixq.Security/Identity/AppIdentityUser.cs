using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Security;
using Ixq.Core.Entity;

namespace Ixq.Security.Identity
{
    public class AppIdentityUser : Microsoft.AspNet.Identity.EntityFramework.IdentityUser, IUser<string>, IUserSpecification
    {
        public AppIdentityUser()
        {
        }

        public AppIdentityUser(string name) : base(name)
        {
        }

        public virtual DateTime? LastSignInDate { get; set; }
        public virtual DateTime? LastSignOutDate { get; set; }
        public virtual void OnSignInComplete()
        {
        }

        public virtual void OnSignOutComplete()
        {
        }
    }
}
