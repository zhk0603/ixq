using System;

namespace Ixq.Core.Logging
{
    /// <summary>
    ///     日志工厂。
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        ILogger Create<T>();

        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        ILogger Create(Type type);

        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILogger Create(string name);
    }
}