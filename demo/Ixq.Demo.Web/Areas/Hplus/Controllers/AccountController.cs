using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
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
        //private readonly ApplicationUserManager _userManager;
        //private readonly ApplicationRoleManager _roleManager;
        //private readonly ApplicationSignInManager _signInManager;

        //public AccountController(IUserStore<ApplicationUser> userStore,
        //    IRoleStore<ApplicationRole, string> roleStore)
        //{
        //    _userManager = new ApplicationUserManager(userStore);
        //    _roleManager = new ApplicationRoleManager(roleStore);
        //}
        //public AccountController(ApplicationRoleManager roleManager, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    _userManager = userManager;
        //    _roleManager = roleManager;
        //    _signInManager = signInManager;
        //}

        //public ApplicationUserManager UserManager { get; set; }
        //public ApplicationRoleManager RoleManager { get; set; }



        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
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


        // GET: Hplus/Account
        public ActionResult Index()
        {
            var a1 = _userManager.GetHashCode();
            var a2 = UserManager.GetHashCode();

            //var b1 = _roleManager.GetHashCode();
            //var b2 = RoleManager.GetHashCode();

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
            var result = await SignInManager.PasswordSignInAsync(userName, password, false, shouldLockout: false);
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
    }
}