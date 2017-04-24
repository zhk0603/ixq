using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Demo.Domain;
using Ixq.Demo.Domain.ApplicationServer;
using Ixq.Demo.Entities;
using Ixq.Demo.Web.Controllers;
using Ixq.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ApplicationRoleManager _roleManager;

        //public AccountController(IUserStore<ApplicationUser> userStore,
        //    IRoleStore<ApplicationRole, string> roleStore)
        //{
        //    _userManager = new ApplicationUserManager(userStore);
        //    _roleManager = new ApplicationRoleManager(roleStore);
        //}
        public AccountController(ApplicationRoleManager roleManager, ApplicationUserManager userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public ApplicationUserManager UserManager { get; set; }
        public ApplicationRoleManager RoleManager { get; set; }

        // GET: Hplus/Account
        public ActionResult Index()
        {
            var a1 = _userManager.GetHashCode();
            var a2 = UserManager.GetHashCode();

            var b1 = _roleManager.GetHashCode();
            var b2 = RoleManager.GetHashCode();

            return View();
        }
    }
}