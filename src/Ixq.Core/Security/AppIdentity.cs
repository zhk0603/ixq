using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;

namespace Ixq.Core.Security
{
    public class AppIdentity : ClaimsIdentity
    {
        public AppIdentity(IIdentity identity) : base(identity)
        {
        }

        public AppIdentity(IEnumerable<Claim> claims) : base(claims)
        {
        }

        public string IdClaimType => ClaimTypes.NameIdentifier;

        public virtual string Id
        {
            get
            {
                var claim = FindFirst(ClaimTypes.NameIdentifier);
                return claim?.Value;
            }
        }

        public virtual CurrentUserWrap UserInfo { get; set; }
    }
}