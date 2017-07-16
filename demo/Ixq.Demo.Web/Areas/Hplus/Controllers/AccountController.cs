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
using Ixq.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using Ixq.Core.Security;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.Security.Identity;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IRoleManager<Security.Identity.IRole> _roleManager;
        private readonly IUserManager<Security.Identity.IUser> _userManager;

        public AccountController(IRoleManager<Security.Identity.IRole> roleManager, IUserManager<Security.Identity.IUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Hplus/Account
        public ActionResult Index()
        {
            var b1 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();
            var b5 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();

            var a1 = _userManager.GetHashCode();
            var a2 = _userManager.GetHashCode();

            var b3 = _roleManager.GetHashCode();
            var b2 = _roleManager.GetHashCode();

            var roles = _roleManager.Roles.ToList();
            var users = _userManager.Users.ToList();
            var aa = _userManager.GetHashCode();

            var b = _userManager is IUserManager<Security.Identity.IUser>;

            //var use = SignInManager;

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
            var result = await ApplicationSignInManager<ApplicationUser>.PasswordSignInAsync(userName, password, false, true);
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
            ApplicationSignInManager<ApplicationUser>.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}