using System;
using Ixq.Core;
using Ixq.Core.DependencyInjection;
using Ixq.DependencyInjection.Autofac;
using Ixq.Mapper.AutoMapper;
using Ixq.Security.Identity;
using Owin;
using System.Reflection;

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

        public static IAppBuilder RegisterAutofac(this IAppBuilder app, params Assembly[] controllerAssemblies)
        {
            AppBootProgram.Instance.RegisterAutofac(controllerAssemblies);
            return app;
        }

        public static IAppBuilder RegisterAutoMappe(this IAppBuilder app)
        {
            AppBootProgram.Instance.RegisterAutoMappe();
            return app;
        }

        public static IAppBuilder RegisterIdentity(this IAppBuilder app, Action<IServiceCollection> action)
        {
            AppBootProgram.Instance.RegisterIdentity(action);
            return app;
        }
    }
}