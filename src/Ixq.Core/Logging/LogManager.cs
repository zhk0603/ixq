using System;
using System.Collections.Concurrent;

namespace Ixq.Core.Logging
{
    /// <summary>
    ///     日志管理器。
    /// </summary>
    public class LogManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        private static ILoggerFactory _loggerFactory;

        static LogManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
        }

        /// <summary>
        ///     设置日志工厂。
        /// </summary>
        /// <param name="factory"></param>
        public static void SetLoggerFactory(ILoggerFactory factory)
        {
            _loggerFactory = factory;
        }

        /// <summary>
        ///     设置日志工厂。
        /// </summary>
        /// <param name="func"></param>
        public static void SetLoggerFactory(Func<ILoggerFactory> func)
        {
            _loggerFactory = func();
        }

        /// <summary>
        ///     获取<see cref="ILogger" />。
        /// </summary>
        /// <param name="name">日志记录器名称。</param>
        /// <returns></returns>
        public static ILogger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            if (_loggerFactory == null)
                return null;

            ILogger logger;
            if (Loggers.TryGetValue(name, out logger))
                return logger;
            logger = _loggerFactory.Create(name);
            Loggers[name] = logger;
            return logger;
        }

        /// <summary>
        ///     获取<see cref="ILogger" />。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static ILogger GetLogger<T>()
        {
            if (_loggerFactory == null)
                return null;
            ILogger logger;
            var name = typeof(T).FullName;
            if (Loggers.TryGetValue(name, out logger))
                return logger;
            logger = _loggerFactory.Create<T>();
            Loggers[name] = logger;
            return logger;
        }

        /// <summary>
        ///     获取<see cref="ILogger" />。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static ILogger GetLogger(Type type)
        {
            if (type == null)
                throw new ArgumentException(nameof(type));
            if (_loggerFactory == null)
                return null;

            var name = type.FullName;
            ILogger logger;
            if (Loggers.TryGetValue(name, out logger))
                return logger;
            logger = _loggerFactory.Create(type);
            Loggers[name] = logger;
            return logger;
        }
    }
}