using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.Owin;

namespace Ixq.Security.Cookies
{
    public class CookieAuthenticationProvider : Microsoft.Owin.Security.Cookies.CookieAuthenticationProvider
    {
        public CookieAuthenticationProvider() : this(
            Ixq.Core.Logging.LogManager.GetLogger("CookieAuthenticationProvider"))
        {
        }

        public CookieAuthenticationProvider(Ixq.Core.Logging.ILogger logger)
        {
            this.Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected virtual Ixq.Core.Logging.ILogger Logger { get; }

        public override void ResponseSignIn(CookieResponseSignInContext context)
        {
            var appIdentity = new Ixq.Core.Security.AppIdentity(context.Identity);
            context.Identity = appIdentity;
            this.OnResponseSignIn.Invoke(context);
        }
        public override void ResponseSignOut(CookieResponseSignOutContext context)
        {
            this.OnResponseSignOut.Invoke(context);
        }
    }
}
