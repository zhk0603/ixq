using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.DataHandler;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Infrastructure;
using Owin;

namespace Ixq.Security.Cookies
{
    public class
        ExtendAuthenticationMiddleware<TManager, TUser> : AuthenticationMiddleware<ExtendAuthenticationOptions<TUser>>
        where TManager : UserManager<TUser, long>
        where TUser : class, IUser<long>, Core.Security.IUser<long>
    {
        public ExtendAuthenticationMiddleware(
            OwinMiddleware next,
            IAppBuilder app,
            ExtendAuthenticationOptions<TUser> options) :
            base(next, options)
        {
            if (Options.TicketDataFormat == null)
            {
                var dataProtector = app.CreateDataProtector(
                    typeof(CookieAuthenticationMiddleware).FullName,
                    Options.AuthenticationType, "v1");

                Options.TicketDataFormat = new TicketDataFormat(dataProtector);
            }
            if (Options.CookieManager == null)
                Options.CookieManager = new ChunkingCookieManager();
        }

        protected override AuthenticationHandler<ExtendAuthenticationOptions<TUser>> CreateHandler()
        {
            return new ExtendAuthenticationHandler<TManager, TUser>();
        }
    }
}