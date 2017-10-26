using System;
using Ixq.Core.Entity;
using Ixq.Core.Security;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Security.Identity
{
    public class AppIdentityUser :
        IdentityUser<long, AppIdentityUserLogin, AppIdentityUserRole, AppIdentityUserClaim>, IUser<long>,
        IUserSpecification
    {
        public AppIdentityUser()
        {
        }

        public AppIdentityUser(string name)
        {
            UserName = name;
        }

        public DateTime? LastSignInDate { get; set; }
        public DateTime? LastSignOutDate { get; set; }

        public void OnSignInComplete()
        {
        }

        public void OnSignOutComplete()
        {
        }
    }
}