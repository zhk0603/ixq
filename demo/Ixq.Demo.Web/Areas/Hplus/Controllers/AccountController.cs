using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
        private readonly ApplicationUserServer _userServer;
        private readonly ApplicationRoleServer _roleServer;
        public AccountController(IUserStore<ApplicationUser> userStore,
            IRoleStore<ApplicationRole, string> roleStore)
        {
            _userServer = new ApplicationUserServer(userStore);
            _roleServer = new ApplicationRoleServer(roleStore);
        }
        public ApplicationUserServer UserServer { get; set; }
        public ApplicationRoleServer RoleServer { get; set; }

        // GET: Hplus/Account
        public ActionResult Index()
        {
            return View();
        }
    }
}