using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Owin;
using Microsoft.Owin.Extensions;

namespace Ixq.Security.Cookies
{
    public static class CookieAuthenticationExtensions
    {
        public static IAppBuilder UseExtendCookieAuthentication<TManager, TUser>(this IAppBuilder app,
            ExtendAuthenticationOptions<TUser> options)
            where TManager : UserManager<TUser, long>
            where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
        {
            return app.UseExtendCookieAuthentication<TManager, TUser>(options, PipelineStage.Authenticate);
        }

        public static IAppBuilder UseExtendCookieAuthentication<TManager, TUser>(this IAppBuilder app,
            ExtendAuthenticationOptions<TUser> options,
            PipelineStage stage)
            where TManager : UserManager<TUser, long>
            where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
        {
            if (app == null)
            {
                throw new ArgumentNullException("app");
            }

            app.Use(typeof(ExtendAuthenticationMiddleware<TManager, TUser>), app, options);
            app.UseStageMarker(stage);
            return app;
        }
    }
}
