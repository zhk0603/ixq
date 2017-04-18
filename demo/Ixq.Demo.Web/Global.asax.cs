﻿using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ixq.Core;
using Ixq.DependencyInjection.Autofac;
using Ixq.Extensions;
using Ixq.Mapper.AutoMapper;

namespace Ixq.Demo.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var app = AppBootProgram<MvcApplication>.Instance;
            //app.Initialization()
            //    .RegisterAutofac()
            //    .RegisterAutoMappe();
        }
    }
}