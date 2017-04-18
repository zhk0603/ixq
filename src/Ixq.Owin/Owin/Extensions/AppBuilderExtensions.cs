using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using System.Web;
using Ixq.Core;
using Ixq.DependencyInjection.Autofac;
using Ixq.Mapper.AutoMapper;

namespace Ixq.Owin.Extensions
{
    public static class AppBuilderExtensions
    {
        public static IAppBuilder Initialization<T>(this IAppBuilder app) where T : class, new()
        {
            AppBootProgram<T>.Instance.Initialization();
            return app;
        }

        public static IAppBuilder RegisterAutofac<T>(this IAppBuilder app) where T : class, new()
        {
            AppBootProgram<T>.Instance.RegisterAutofac();
            return app;
        }
        public static IAppBuilder RegisterAutoMappe<T>(this IAppBuilder app) where T : class, new()
        {
            AppBootProgram<T>.Instance.RegisterAutoMappe();
            return app;
        }
    }
}
