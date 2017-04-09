using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.DependencyInjection;
using Ixq.Core.DependencyInjection.Extensions;
using Ixq.DependencyInjection.Autofac;

namespace Ixq.Demo.Web.Controllers
{
    public class TestController : BaseController
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
    }
}