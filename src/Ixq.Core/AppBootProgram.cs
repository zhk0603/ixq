using Ixq.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;

namespace Ixq.Core
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public class AppBootProgram
    {
        private Type[] _scopeAssembly;
        private Type[] _singletonAssembly;
        private Type[] _transientAssembly;
        private readonly AssemblyFinder _assemblyFinder;

        public AppBootProgram()
        {
            _assemblyFinder = new AssemblyFinder();
        }

        public AppBootProgram Initialization()
        {
            var assemblies = _assemblyFinder.FindAll();
            _scopeAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Scoped);
            _singletonAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Singleton);
            _transientAssembly = SelectManyForEnumLifetimeStyle(assemblies, ServiceLifetime.Transient);

            return this;
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
