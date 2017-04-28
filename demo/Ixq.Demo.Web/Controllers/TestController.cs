using System;
using System.Web.Mvc;

namespace Ixq.Demo.Web.Controllers
{
    [Authorize]
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Exception()
        {
            throw new Exception("Exp");
            return View();
        }
    }
}