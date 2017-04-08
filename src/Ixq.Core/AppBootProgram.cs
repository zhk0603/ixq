using Ixq.Extended;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.DependencyInjection.Extensions;

namespace Ixq.Core
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public class AppBootProgram
    {
        private IServiceCollection _serviceCollection;
        private Type[] _scopeAssembly;
        private Type[] _singletonAssembly;
        private Type[] _transientAssembly;
        private readonly AssemblyFinder _assemblyFinder;

        public IServiceCollection ServiceCollection { get { return _serviceCollection; } }

        /// <summary>
        ///     初始化一个<see cref="AppBootProgram"/>实例
        /// </summary>
        public AppBootProgram()
        {
            _assemblyFinder = new AssemblyFinder();
            _serviceCollection = new ServiceCollection();
        }

        public AppBootProgram Initialization()
        {
            var assemblies = _assemblyFinder.FindAll();
            _scopeAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Scoped);
            _singletonAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Singleton);
            _transientAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Transient);

            AddTypeWithInterfaces(_serviceCollection, _scopeAssembly, ServiceLifetime.Scoped);
            AddTypeWithInterfaces(_serviceCollection, _singletonAssembly, ServiceLifetime.Singleton);
            AddTypeWithInterfaces(_serviceCollection, _transientAssembly, ServiceLifetime.Transient);
            return this;
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
        protected virtual void AddTypeWithInterfaces(IServiceCollection services, Type[] implementationTypes,
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
                            services.AddTransient(implementationType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(implementationType, implementationType);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(implementationType, implementationType);
                            break;
                    }
                    continue;
                }
                foreach (var interfaceType in interfaceTypes)
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Transient:
                            services.AddTransient(interfaceType, implementationType);
                            break;
                        case ServiceLifetime.Scoped:
                            services.AddScoped(interfaceType, implementationType);
                            break;
                        case ServiceLifetime.Singleton:
                            services.AddSingleton(interfaceType, implementationType);
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
    }
}
