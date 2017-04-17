// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Ixq.Core.DependencyInjection
{
    /// <summary>
    /// Default implementation of <see cref="IServiceCollection"/>.
    /// </summary>
    public class ServiceCollection : IServiceCollection
    {
        private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();

        /// <inheritdoc />
        public int Count => _descriptors.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        public ServiceDescriptor this[int index]
        {
            get
            {
                return _descriptors[index];
            }
            set
            {
                _descriptors[index] = value;
            }
        }

        public void Init(Assembly[] assembly)
        {
            var scopeAssembly = SelectManyForEnumLifetimeStyle(assembly, ServiceLifetime.Scoped);
            var singletonAssembly = SelectManyForEnumLifetimeStyle(assembly, ServiceLifetime.Singleton);
            var transientAssembly = SelectManyForEnumLifetimeStyle(assembly, ServiceLifetime.Transient);

            AddTypeWithInterfaces(scopeAssembly, ServiceLifetime.Scoped);
            AddTypeWithInterfaces(singletonAssembly, ServiceLifetime.Singleton);
            AddTypeWithInterfaces(transientAssembly, ServiceLifetime.Transient);
        }

        /// <inheritdoc />
        public void Clear()
        {
            _descriptors.Clear();
        }

        /// <inheritdoc />
        public bool Contains(ServiceDescriptor item)
        {
            return _descriptors.Contains(item);
        }

        /// <inheritdoc />
        public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
        {
            _descriptors.CopyTo(array, arrayIndex);
        }

        /// <inheritdoc />
        public bool Remove(ServiceDescriptor item)
        {
            return _descriptors.Remove(item);
        }

        /// <inheritdoc />
        public IEnumerator<ServiceDescriptor> GetEnumerator()
        {
            return _descriptors.GetEnumerator();
        }

        void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
        {
            _descriptors.Add(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(ServiceDescriptor item)
        {
            return _descriptors.IndexOf(item);
        }

        public void Insert(int index, ServiceDescriptor item)
        {
            _descriptors.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _descriptors.RemoveAt(index);
        }

        #region parvate method
        private static Type[] SelectManyForEnumLifetimeStyle(Assembly[] assemblies, ServiceLifetime lifetime)
        {
            Type[] type = { };
            switch (lifetime)
            {
                case ServiceLifetime.Scoped:
                    type = assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(t =>
                            typeof(IScopeDependency).IsAssignableFrom(t) && !t.IsAbstract))
                        .Distinct().ToArray();
                    break;
                case ServiceLifetime.Singleton:
                    type = assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(t =>
                            typeof(ISingletonDependency).IsAssignableFrom(t) && !t.IsAbstract))
                        .Distinct().ToArray();
                    break;
                case ServiceLifetime.Transient:
                    type = assemblies.SelectMany(assembly =>
                        assembly.GetTypes().Where(t =>
                            typeof(ITransientDependency).IsAssignableFrom(t) && !t.IsAbstract))
                        .Distinct().ToArray();
                    break;
            }

            return type;
        }
        /// <summary>
        ///     以类型实现的接口进行服务添加，需排除
        ///     <see cref="ITransientDependency" />、
        ///     <see cref="IScopeDependency" />、
        ///     <see cref="ISingletonDependency" />、
        ///     <see cref="IDependency" />、
        ///     <see cref="IDisposable" />等非业务接口，如无接口则注册自身
        /// </summary>
        /// <param name="services">服务映射信息集合</param>
        /// <param name="implementationTypes">要注册的实现类型集合</param>
        /// <param name="lifetime">注册的生命周期类型</param>
        protected virtual void AddTypeWithInterfaces(Type[] implementationTypes,
            ServiceLifetime lifetime)
        {
            foreach (var implementationType in implementationTypes)
            {
                if (implementationType.IsAbstract || implementationType.IsInterface)
                {
                    continue;
                }
                var interfaceTypes = GetImplementedInterfaces(implementationType);
                if (interfaceTypes.Length == 0)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Transient:
                            this.AddTransient(implementationType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            this.AddScoped(implementationType, implementationType);
                            break;
                        case ServiceLifetime.Singleton:
                            this.AddSingleton(implementationType, implementationType);
                            break;
                    }
                    continue;
                }
                foreach (var interfaceType in interfaceTypes)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Transient:
                            this.AddTransient(interfaceType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            this.AddScoped(interfaceType, implementationType);
                            break;
                        case ServiceLifetime.Singleton:
                            this.AddSingleton(interfaceType, implementationType);
                            break;
                    }
                }
            }
        }
        private static Type[] GetImplementedInterfaces(Type type)
        {
            Type[] exceptInterfaces =
            {
                typeof (IDisposable),
                typeof (IDependency),
                typeof (ITransientDependency),
                typeof (IScopeDependency),
                typeof (ISingletonDependency)
            };
            var interfaceTypes = type.GetInterfaces().Where(m => !exceptInterfaces.Contains(m)).ToArray();
            for (var index = 0; index < interfaceTypes.Length; index++)
            {
                var interfaceType = interfaceTypes[index];
                if (interfaceType.IsGenericType && !interfaceType.IsGenericTypeDefinition &&
                    interfaceType.FullName == null)
                {
                    interfaceTypes[index] = interfaceType.GetGenericTypeDefinition();
                }
            }
            return interfaceTypes;
        }

        #endregion
    }
}
