using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Demo.Entities;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        // GET: Hplus/Home
        public ActionResult Index()
        {
            var user = User.Identity.IsAuthenticated;
            var a = User.IsInRole("Supervisor");

            return View();
        }
        public ActionResult Welcome()
        {
            var userStory = new UserStore<ApplicationUser>();
            var user = userStory.Users;
            return View();
        }
    }
}