using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.DependencyInjection;

namespace Ixq.Demo.Web.Controllers
{

    public class BaseController : Controller
    {
        public IServiceProvider ServiceProvider { get; set; }


        public BaseController(IServiceProvider p)
        {

            var fa = (IServiceScopeFactory)ServiceProvider.GetService(typeof (IServiceScopeFactory));
        }

        // GET: Base
        public ActionResult Index()
        {

            //ServiceProvider.GetService<IServiceProvider>();

            return View();
        }
    }
}