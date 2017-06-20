using System;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public abstract class IdentityUserBase : IdentityUser, IUser<string>
    {
        protected IdentityUserBase() { }
        protected IdentityUserBase(string userName) : base(userName) { }

        public DateTime? LastSignInDate { get; set; }
        public DateTime? LastSignOutDate { get; set; }
        public void OnSignInComplete()
        {
            throw new NotImplementedException();
        }

        public void OnSignOutComplete()
        {
            throw new NotImplementedException();
        }
    }
}