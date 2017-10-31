using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;

namespace Ixq.Security.Cookies
{
    public class ExtendAuthenticationMiddleware<TManager, TUser> : AuthenticationMiddleware<ExtendAuthenticationOptions<TUser>>
        where TManager : UserManager<TUser, long>
        where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
    {
        public ExtendAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            ExtendAuthenticationOptions<TUser> options) :
            base(next, options)
        {
            if (Options.TicketDataFormat == null)
            {
                IDataProtector dataProtector = app.CreateDataProtector(
                    typeof(CookieAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");

                Options.TicketDataFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.CookieManager == null)
            {
                Options.CookieManager = new ChunkingCookieManager();
            }
        }

        protected override AuthenticationHandler<ExtendAuthenticationOptions<TUser>> CreateHandler()
        {
            return new ExtendAuthenticationHandler<TManager, TUser>();
        }
    }
}
