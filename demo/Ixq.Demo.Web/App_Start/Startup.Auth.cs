using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using ixq.Demo.DbContext;
using Ixq.Core;
using Ixq.Demo.Domain;
using Ixq.Demo.Entities;
using Ixq.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using Ixq.Web.Mvc;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Core.Security;
using Ixq.Security.Cookies;

namespace Ixq.Demo.Web
{
    public partial class Startup
    {
        // 有关配置身份验证的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // 配置数据库上下文、用户管理器和登录管理器，以便为每个请求使用单个实例
            app.CreatePerOwinContext(DataContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // 使应用程序可以使用 Cookie 来存储已登录用户的信息
            // 并使用 Cookie 来临时存储有关使用第三方登录提供程序登录的用户的信息
            // 配置登录 Cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                CookieName = "IxqApplicationCookie",
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Hplus/Account/Login"),
                Provider = new Ixq.Security.Cookies.CookieAuthenticationProvider
                {
                    // 当用户登录时使应用程序可以验证安全戳。
                    // 这是一项安全功能，当你更改密码或者向帐户添加外部登录名时，将使用此功能。
                    OnValidateIdentity =
                        Ixq.Security.Owin.SecurityStampValidator
                            .OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                                validateInterval: TimeSpan.FromMinutes(30),
                                regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });

            app.UseAppCookieAuthentication<ApplicationUserManager, ApplicationUser>(
                new AppAuthenticationOptions<ApplicationUser>());

        }

        private CurrentUserWrap GetCurrentUser()
        {
            var context = (DataContext) Core.DependencyInjection.ServiceProvider.Current.GetService<DbContext>();
            var userId = Thread.CurrentPrincipal.Identity.GetUserId<long>();
            var user = context.Users.SingleOrDefault(x => x.Id == userId);
            var warpUser = new CurrentUserWrap();
            warpUser.UserId = user.Id.ToString();
            warpUser.UserName = user.UserName;
            warpUser.PhoneNumber = user.PhoneNumber;
            warpUser.Email = user.Email;
            warpUser.LoginTime = DateTime.Now;
            warpUser.LoginIp = NetHelper.CurrentIp;
            warpUser.ClaimsPrincipal = Thread.CurrentPrincipal as AppPrincipal;
            return warpUser;
        }
    }
}