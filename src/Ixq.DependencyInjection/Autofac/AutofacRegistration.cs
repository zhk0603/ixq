using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Builder;
using Autofac.Integration.Mvc;
using Ixq.Core;
using Ixq.Core.DependencyInjection;

namespace Ixq.DependencyInjection.Autofac
{
    public static class AutofacRegistration
    {
        public static AppBootProgram RegisterAutofac(this AppBootProgram app, Type httpApplicationType)
        {
            RegisterAutofacInternal(app.ServiceCollection, httpApplicationType.Assembly);
            return app;
        }

        public static AppBootProgram RegisterAutofac(this AppBootProgram app, params Assembly[] controllerAssemblies)
        {
            RegisterAutofacInternal(app.ServiceCollection, controllerAssemblies);
            return app;
        }

        internal static void RegisterAutofacInternal(IServiceCollection serviceCollection,
            params Assembly[] controllerAssemblies)
        {
            var builder = new ContainerBuilder();
            builder.RegisterFilterProvider();
            builder.Populate(serviceCollection);
            builder.RegisterControllers(controllerAssemblies)
                .PropertiesAutowired();
            var container = builder.Build();
            var resolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(resolver);
            ServiceProvider.SetProvider(() => resolver.GetService<IServiceProvider>());
        }

        private static void Populate(
            this ContainerBuilder builder, IEnumerable<ServiceDescriptor> descriptors)
        {
            builder.RegisterType<AutofacServiceProvider>().As<IServiceProvider>();
            builder.RegisterType<AutofacServiceScopeFactory>().As<IServiceScopeFactory>();
            Register(builder, descriptors);
        }

        private static IRegistrationBuilder<object, T, U> ConfigureLifecycle<T, U>(
            this IRegistrationBuilder<object, T, U> registrationBuilder,
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

        private static IRegistrationBuilder<object, T, U> ConfigureName<T, U>(
            this IRegistrationBuilder<object, T, U> registrationBuilder,
            ServiceDescriptor serviceDescriptor)
        {
            if (!string.IsNullOrWhiteSpace(serviceDescriptor.Alias))
                registrationBuilder.Named(serviceDescriptor.Alias, serviceDescriptor.ServiceType);

            return registrationBuilder;
        }


        private static void Register(
            ContainerBuilder builder,
            IEnumerable<ServiceDescriptor> descriptors)
        {
            foreach (var descriptor in descriptors)
                if (descriptor.ImplementationType != null)
                {
                    var serviceTypeInfo = descriptor.ServiceType.GetTypeInfo();
                    if (serviceTypeInfo.IsGenericTypeDefinition)
                        builder.RegisterGeneric(descriptor.ImplementationType)
                            .As(descriptor.ServiceType)
                            .AsSelf()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                            .ConfigureLifecycle(descriptor.Lifetime)
                            .ConfigureName(descriptor);
                    else
                        builder.RegisterType(descriptor.ImplementationType)
                            .As(descriptor.ServiceType)
                            .AsSelf()
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                            .ConfigureLifecycle(descriptor.Lifetime)
                            .ConfigureName(descriptor);
                }
                else if (descriptor.ImplementationFactory != null)
                {
                    var registration = RegistrationBuilder.ForDelegate(descriptor.ServiceType,
                            (context, paramters) =>
                            {
                                var provider = context.Resolve<IServiceProvider>();
                                return descriptor.ImplementationFactory(provider);
                            })
                        .ConfigureLifecycle(descriptor.Lifetime)
                        .ConfigureName(descriptor)
                        .CreateRegistration();
                    builder.RegisterComponent(registration);
                }
                else if (descriptor.ImplementationInstance != null)
                {
                    builder.RegisterInstance(descriptor.ImplementationInstance)
                        .As(descriptor.ServiceType)
                        .AsSelf()
                        .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies)
                        .ConfigureLifecycle(descriptor.Lifetime)
                        .ConfigureName(descriptor);
                }
        }
    }
}