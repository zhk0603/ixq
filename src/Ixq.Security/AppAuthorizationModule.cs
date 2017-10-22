using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using Ixq.Core.Security;

namespace Ixq.Security
{
    public class AppAuthorizationModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.AuthorizeRequest += Context_AuthorizeRequest;
        }

        private void Context_AuthorizeRequest(object sender, EventArgs e)
        {
            var httpContext = sender as HttpApplication;
            if (httpContext?.Context.User != null && httpContext.Context.User.Identity.IsAuthenticated)
            {
                var claimsPrincipal = httpContext.Context.User as ClaimsPrincipal;
                if (claimsPrincipal != null)
                {
                    var appIdentities = claimsPrincipal.Identities.Select(x => new AppIdentity(x));
                    var appPrincipal = new AppPrincipal(appIdentities);
                    httpContext.Context.User = appPrincipal;
                    Thread.CurrentPrincipal = appPrincipal;
                }
            }
        }
    }
}
