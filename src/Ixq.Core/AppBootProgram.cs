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

        /// <summary>
        ///     实现单例。
        /// </summary>
        public static AppBootProgram Instance => LazyBootprogram.Value;

        /// <summary>
        ///     服务集合。
        /// </summary>
        public IServiceCollection ServiceCollection { get; }

        /// <summary>
        ///     映射集合。
        /// </summary>
        public IMapperCollection MapperCollection { get; }

        /// <summary>
        ///     初始化框架。
        /// </summary>
        /// <returns></returns>
        public AppBootProgram Initialization()
        {
            var assemblies = _assemblyFinder.FindAll();
            ServiceCollection.Init(assemblies);
            MapperCollection.Init(assemblies);

            return this;
        }
    }
}