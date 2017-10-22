using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Security
{
    public class AppPrincipal : ClaimsPrincipal
    {
        public AppPrincipal(IEnumerable<AppIdentity> identities) : base(identities)
        {
        }

        public new AppIdentity Identity
        {
            get
            {
                var identity = base.Identity;
                return identity as AppIdentity;
            }
        }
    }
}
