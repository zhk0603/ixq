using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.DependencyInjection.Extensions;
using System.Web;
using Ixq.Core.Mapper;
using Ixq.Extensions;

namespace Ixq.Core
{
    /// <summary>
    /// 引导程序
    /// </summary>
    public class AppBootProgram<T> where T : HttpApplication, new()
    {
        private readonly IMapperCollection _mapperCollection;
        private readonly IServiceCollection _serviceCollection;
        private readonly AssemblyFinder _assemblyFinder;

        public IServiceCollection ServiceCollection => _serviceCollection;
        public IMapperCollection MapperCollection => _mapperCollection;

        /// <summary>
        ///     初始化一个<see cref="AppBootProgram{T}"/>实例
        /// </summary>
        public AppBootProgram()
        {
            _assemblyFinder = new AssemblyFinder();
            _serviceCollection = new ServiceCollection();
            _mapperCollection = new MapperCollection();
        }

        public AppBootProgram<T> Initialization()
        {
            var assemblies = _assemblyFinder.FindAll();
            _serviceCollection.Init(assemblies);
            _mapperCollection.Init(assemblies);

            return this;
        }
    }
}
