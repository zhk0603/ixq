using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ixq.Demo.Web.Areas.Hplus.Controllers
{
    public class HomeController : Controller
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