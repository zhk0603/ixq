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

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private IRoleManager<Security.Identity.IRole> _roleManager;
        private IUserManager<Security.Identity.IUser> _userManager;

        public AccountController(IRoleManager<Security.Identity.IRole> roleManager, IUserManager<Security.Identity.IUser> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? (_signInManager = HttpContext.GetOwinContext().Get<ApplicationSignInManager>());
            }
            private set
            {
                _signInManager = value;
            }
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
            var result = await SignInManager.PasswordSignInAsync(userName, password, false, shouldLockout: true);
            switch (result)
            {
                case Microsoft.AspNet.Identity.Owin.SignInStatus.Success:
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
            SignInManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}