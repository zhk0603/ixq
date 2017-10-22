using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Security;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppIdentityUser : IdentityUser, IUser<string>, IUserSpecification
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
