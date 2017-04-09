using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ixq.Core.DependencyInjection;

namespace Ixq.Demo.Web.Controllers
{

    public abstract class BaseController : Controller
    {
        public IServiceProvider ServiceProvider { get; set; }
    }
}