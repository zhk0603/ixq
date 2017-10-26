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
    public class AppAuthenticationMiddleware<TManager, TUser> : AuthenticationMiddleware<AppAuthenticationOptions<TUser>>
        where TManager : UserManager<TUser, long>
        where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
    {
        public AppAuthenticationMiddleware(OwinMiddleware next, IAppBuilder app,
            AppAuthenticationOptions<TUser> options) :
            base(next, options)
        {
            if (Options.TicketDataFormat == null)
            {
                IDataProtector dataProtector = app.CreateDataProtector(
                    typeof(AppAuthenticationMiddleware<TManager, TUser>).FullName,
                    Options.AuthenticationType, "v1");

                Options.TicketDataFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.CookieManager == null)
            {
                Options.CookieManager = new ChunkingCookieManager();
            }
        }

        protected override AuthenticationHandler<AppAuthenticationOptions<TUser>> CreateHandler()
        {
            return new AppAuthenticationHandler<TManager, TUser>();
        }
    }
}
