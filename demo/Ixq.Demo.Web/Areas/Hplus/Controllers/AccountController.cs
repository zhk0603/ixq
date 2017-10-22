using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Ixq.Demo.Domain;
using Ixq.Demo.Entities;
using Ixq.Demo.Web.Controllers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Ixq.Core.DependencyInjection.Extensions;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class AccountController : BaseController
    {
        public ApplicationSignInManager SignInManager => HttpContext.GetOwinContext().Get<ApplicationSignInManager>();

        // GET: Hplus/Account
        public ActionResult Index()
        {
            var b1 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();
            var b5 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();

            return View();
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string userName, string password, string code, string returnUrl)
        {
            var result = await SignInManager.PasswordSignInAsync(userName, password, false, true);
            switch (result)
            {
                case SignInStatus.Success:
                    if (string.IsNullOrWhiteSpace(returnUrl))
                        return RedirectToAction("Index", "Home");
                    return Redirect(returnUrl);
                default:
                    ViewBag.ErrorMessage = "登录失败";
                    return View();
            }
        }

        public ActionResult Logout()
        {
            Ixq.Core.Cache.CacheManager.GetCache("LoginUser")
                .Remove(Ixq.Core.CurrentUser.Current.UserId);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;
    }
}