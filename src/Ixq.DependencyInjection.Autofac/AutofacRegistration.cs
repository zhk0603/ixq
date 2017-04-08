using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Ixq.Core.DependencyInjection;
using Ixq.Core;
using Ixq.Extended;

namespace Ixq.DependencyInjection.Autofac
{
    public static class AutofacRegistration
    {
        public static AppBootProgram RegisterAutofac(this AppBootProgram app)
        {
            var assemblyFinder = new AssemblyFinder();
            var builder = new ContainerBuilder();
            builder.RegisterControllers(assemblyFinder.FindAll()).AsSelf().PropertiesAutowired();
            builder.RegisterFilterProvider();
            builder.Populate(app.ServiceCollection);
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            return app;
        }

        private static void Populate(
            this ContainerBuilder builder, IEnumerable<ServiceDescriptor> descriptors)
        {
            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>();
            builder.RegisterType<AutofacServiceScopeFactory>().As<IServiceScopeFactory>();

            Register(builder, descriptors);
        }

        private static IRegistrationBuilder<object, T, SingleRegistrationStyle> ConfigureLifecycle<T>(
            this IRegistrationBuilder<object, T, SingleRegistrationStyle> registrationBuilder,
            ServiceLifetime lifecycleKind)
        {
            switch (lifecycleKind)
            {
                case ServiceLifetime.Singleton:
                    registrationBuilder.SingleInstance();
                    break;
                case ServiceLifetime.Scoped:
                    registrationBuilder.InstancePerLifetimeScope();
                    break;
                case ServiceLifetime.Transient:
                    registrationBuilder.InstancePerDependency();
                    break;
            }

            return registrationBuilder;
        }


        private static void Register(
            ContainerBuilder builder,
            IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
            {
                if (descriptor.ImplementationType != null)
                {
                    builder
                        .RegisterType(descriptor.ImplementationType)
                        .As(descriptor.ServiceType)
                        .ConfigureLifecycle(descriptor.Lifetime);
                }
                else
                {
                    builder
                        .RegisterInstance(descriptor.ImplementationInstance)
                        .As(descriptor.ServiceType)
                        .ConfigureLifecycle(descriptor.Lifetime);
                }
            }
        }
    }
}
