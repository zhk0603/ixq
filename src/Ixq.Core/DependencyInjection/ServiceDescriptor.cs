// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Diagnostics;

namespace Ixq.Core.DependencyInjection
{
    [DebuggerDisplay("Lifetime = {Lifetime}, ServiceType = {ServiceType}, ImplementationType = {ImplementationType}")]
    public class ServiceDescriptor
    {
        /// <summary>
        ///     Initializes a new instance of <see cref="ServiceDescriptor" /> with the specified
        ///     <paramref name="implementationType" />.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type" /> of the service.</param>
        /// <param name="implementationType">The <see cref="Type" /> implementing the service.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime" /> of the service.</param>
        /// <param name="alias"></param>
        public ServiceDescriptor(
            Type serviceType,
            Type implementationType,
            ServiceLifetime lifetime,
            string alias = null)
            : this(serviceType, lifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }
            Alias = alias;
            ImplementationType = implementationType ?? throw new ArgumentNullException(nameof(implementationType));
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="ServiceDescriptor" /> with the specified <paramref name="instance" />
        ///     as a <see cref="ServiceLifetime.Singleton" />.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type" /> of the service.</param>
        /// <param name="instance">The instance implementing the service.</param>
        /// <param name="alias"></param>
        public ServiceDescriptor(
            Type serviceType,
            object instance,
            string alias = null)
            : this(serviceType, ServiceLifetime.Singleton)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Alias = alias;
            ImplementationInstance = instance ?? throw new ArgumentNullException(nameof(instance));
        }

        /// <summary>
        ///     Initializes a new instance of <see cref="ServiceDescriptor" /> with the specified <paramref name="factory" />.
        /// </summary>
        /// <param name="serviceType">The <see cref="Type" /> of the service.</param>
        /// <param name="factory">A factory used for creating service instances.</param>
        /// <param name="lifetime">The <see cref="ServiceLifetime" /> of the service.</param>
        public ServiceDescriptor(
            Type serviceType,
            Func<IServiceProvider, object> factory,
            ServiceLifetime lifetime,
            string alias = null)
            : this(serviceType, lifetime)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            Alias = alias;
            ImplementationFactory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        private ServiceDescriptor(Type serviceType, ServiceLifetime lifetime)
        {
            Lifetime = lifetime;
            ServiceType = serviceType;
        }

        /// <inheritdoc />
        public string Alias { get; set; }

        /// <inheritdoc />
        public ServiceLifetime Lifetime { get; }

        /// <inheritdoc />
        public Type ServiceType { get; }

        /// <inheritdoc />
        public Type ImplementationType { get; }

        /// <inheritdoc />
        public object ImplementationInstance { get; }

        /// <inheritdoc />
        public Func<IServiceProvider, object> ImplementationFactory { get; }

        internal Type GetImplementationType()
        {
            if (ImplementationType != null)
            {
                return ImplementationType;
            }
            if (ImplementationInstance != null)
            {
                return ImplementationInstance.GetType();
            }
            if (ImplementationFactory != null)
            {
                var typeArguments = ImplementationFactory.GetType().GenericTypeArguments;

                Debug.Assert(typeArguments.Length == 2);

                return typeArguments[1];
            }

            Debug.Assert(false, "ImplementationType, ImplementationInstance or ImplementationFactory must be non null");
            return null;
        }

        public static ServiceDescriptor Transient<TService, TImplementation>(string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Transient, alias);
        }

        public static ServiceDescriptor Transient(Type service, Type implementationType, string alias = null)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Transient, alias);
        }

        public static ServiceDescriptor Transient<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory, string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient, alias);
        }

        public static ServiceDescriptor Transient<TService>(Func<IServiceProvider, TService> implementationFactory,
            string alias = null)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Transient, alias);
        }

        public static ServiceDescriptor Transient(Type service,
            Func<IServiceProvider, object> implementationFactory, string alias = null)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Transient, alias);
        }

        public static ServiceDescriptor Scoped<TService, TImplementation>(string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Scoped, alias);
        }

        public static ServiceDescriptor Scoped(Type service, Type implementationType, string alias = null)
        {
            return Describe(service, implementationType, ServiceLifetime.Scoped, alias);
        }

        public static ServiceDescriptor Scoped<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory, string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped, alias);
        }

        public static ServiceDescriptor Scoped<TService>(Func<IServiceProvider, TService> implementationFactory,
            string alias = null)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Scoped, alias);
        }

        public static ServiceDescriptor Scoped
        (Type service,
            Func<IServiceProvider, object> implementationFactory, string alias = null)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(service, implementationFactory, ServiceLifetime.Scoped, alias);
        }

        public static ServiceDescriptor Singleton<TService, TImplementation>(string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe<TService, TImplementation>(ServiceLifetime.Singleton, alias);
        }

        public static ServiceDescriptor Singleton(Type service, Type implementationType, string alias = null)
        {
            if (service == null)
            {
                throw new ArgumentNullException(nameof(service));
            }

            if (implementationType == null)
            {
                throw new ArgumentNullException(nameof(implementationType));
            }

            return Describe(service, implementationType, ServiceLifetime.Singleton, alias);
        }

        public static ServiceDescriptor Singleton<TService, TImplementation>(
            Func<IServiceProvider, TImplementation> implementationFactory, string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton, alias);
        }

        public static ServiceDescriptor Singleton<TService>(Func<IServiceProvider, TService> implementationFactory,
            string alias = null)
            where TService : class
        {
            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(typeof(TService), implementationFactory, ServiceLifetime.Singleton, alias);
        }

        public static ServiceDescriptor Singleton(
            Type serviceType,
            Func<IServiceProvider, object> implementationFactory,
            string alias = null)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationFactory == null)
            {
                throw new ArgumentNullException(nameof(implementationFactory));
            }

            return Describe(serviceType, implementationFactory, ServiceLifetime.Singleton, alias);
        }

        public static ServiceDescriptor Singleton<TService>(TService implementationInstance, string alias = null)
            where TService : class
        {
            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return Singleton(typeof(TService), implementationInstance, alias);
        }

        public static ServiceDescriptor Singleton(
            Type serviceType,
            object implementationInstance,
            string alias = null)
        {
            if (serviceType == null)
            {
                throw new ArgumentNullException(nameof(serviceType));
            }

            if (implementationInstance == null)
            {
                throw new ArgumentNullException(nameof(implementationInstance));
            }

            return new ServiceDescriptor(serviceType, implementationInstance, alias);
        }

        private static ServiceDescriptor Describe<TService, TImplementation>(ServiceLifetime lifetime,
            string alias = null)
            where TService : class
            where TImplementation : class, TService
        {
            return Describe(
                typeof(TService),
                typeof(TImplementation), lifetime, alias);
        }

        public static ServiceDescriptor Describe(Type serviceType, Type implementationType, ServiceLifetime lifetime,
            string alias = null)
        {
            return new ServiceDescriptor(serviceType, implementationType, lifetime, alias);
        }

        public static ServiceDescriptor Describe(Type serviceType, Func<IServiceProvider, object> implementationFactory,
            ServiceLifetime lifetime, string alias = null)
        {
            return new ServiceDescriptor(serviceType, implementationFactory, lifetime, alias);
        }
    }
}