using System;
using Ixq.Core;
using Ixq.DependencyInjection.Autofac;
using Ixq.Mapper.AutoMapper;
using Owin;

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