using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Ixq.Core.Security
{
    public class AppPrincipal : ClaimsPrincipal
    {
        public AppPrincipal(IEnumerable<AppIdentity> identities) : base(identities)
        {
        }

        public AppPrincipal(IIdentity identity)
        {
            var appIdentity = new AppIdentity(identity);
            AddIdentity(appIdentity);
        }

        public new AppIdentity Identity
        {
            get
            {
                var identity = base.Identity;
                return identity as AppIdentity;
            }
        }

        public virtual string FindFirstValue(string type)
        {
            var claim = FindFirst(type);
            return claim?.Value;
        }
    }
}