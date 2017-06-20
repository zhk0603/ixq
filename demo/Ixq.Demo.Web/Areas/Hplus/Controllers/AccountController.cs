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

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class AccountController : BaseController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationRoleManager _roleManager;
        private ApplicationUserManager _userManager;

        public AccountController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? HttpContext.GetOwinContext().Get<ApplicationRoleManager>();
            }
            private set
            {
                _roleManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;


        // GET: Hplus/Account
        public ActionResult Index()
        {
            var b1 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();
            var b5 = HttpContext.GetOwinContext().Get<ApplicationRoleManager>().GetHashCode();

            var a1 = _userManager.GetHashCode();
            var a2 = UserManager.GetHashCode();

            var b3 = _roleManager.GetHashCode();
            var b2 = RoleManager.GetHashCode();

            var roles = RoleManager.Roles.ToList();
            var users = UserManager.Users.ToList();

            var use = SignInManager.CurrentUser;

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
            SignInManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Login");
        }
    }
}