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
        public static IAppBuilder Initialization(this IAppBuilder app)
        {
            AppBootProgram.Instance.Initialization();
            return app;
        }

        public static IAppBuilder RegisterAutofac(this IAppBuilder app, Type httpApplicationType)
        {
            AppBootProgram.Instance.RegisterAutofac(httpApplicationType);
            return app;
        }
        public static IAppBuilder RegisterAutoMappe(this IAppBuilder app)
        {
            AppBootProgram.Instance.RegisterAutoMappe();
            return app;
        }
    }
}
