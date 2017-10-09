using System;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public abstract class IdentityUserBase : IdentityUser, IUser, IEntity<string>
    {
        protected IdentityUserBase()
        {
        }

        protected IdentityUserBase(string userName) : base(userName)
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