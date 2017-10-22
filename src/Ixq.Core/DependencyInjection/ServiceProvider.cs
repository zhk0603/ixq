using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.DependencyInjection
{
    public class ServiceProvider
    {
        private static IServiceProvider _serviceProvider;
        private static Func<IServiceProvider> _getProvider;

        /// <summary>
        ///     获取依赖解析实例。
        /// </summary>
        public static IServiceProvider Current => _serviceProvider ?? _getProvider();

        /// <summary>
        ///     设置依赖解析接口。
        /// </summary>
        /// <param name="serviceProvider"></param>
        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        /// <summary>
        ///     使用指定的服务委托，设置依赖解析器。
        /// </summary>
        /// <param name="getProvider"></param>
        public static void SetProvider(Func<IServiceProvider> getProvider)
        {
            _getProvider = getProvider;
        }
    }
}
