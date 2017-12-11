using System;

namespace Ixq.Core.DependencyInjection
{
    /// <summary>
    ///     支持“别名”的服务提供者。
    /// </summary>
    public interface ISupportAliasServiceProvider
    {
        /// <summary>
        ///     根据别名获取指定类型的服务对象。
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="alias"></param>
        /// <returns></returns>
        object GetService(Type serviceType, string alias);
    }
}