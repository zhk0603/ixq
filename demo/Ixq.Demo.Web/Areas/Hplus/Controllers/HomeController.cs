using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Demo.Entities;
using Ixq.Demo.Web.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        // GET: Hplus/Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Welcome()
        {
            return View();
        }
    }
}