using System;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Mapper;
using Ixq.Extensions;

namespace Ixq.Core
{
    /// <summary>
    ///     引导程序
    /// </summary>
    public class AppBootProgram
    {
        private static readonly Lazy<AppBootProgram> LazyBootprogram =
            new Lazy<AppBootProgram>(() => new AppBootProgram());

        private readonly AssemblyFinder _assemblyFinder;

        /// <summary>
        ///     初始化一个<see cref="AppBootProgram" />实例
        /// </summary>
        public AppBootProgram()
        {
            _assemblyFinder = new AssemblyFinder();
            ServiceCollection = new ServiceCollection();
            MapperCollection = new MapperCollection();
        }

        public static AppBootProgram Instance => LazyBootprogram.Value;

        public IServiceCollection ServiceCollection { get; }

        public IMapperCollection MapperCollection { get; }

        public AppBootProgram Initialization()
        {
            var assemblies = _assemblyFinder.FindAll();
            ServiceCollection.Init(assemblies);
            MapperCollection.Init(assemblies);

            return this;
        }
    }
}