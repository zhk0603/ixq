using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Logging
{
    public class LogManager
    {
        private static readonly ConcurrentDictionary<string, ILogger> Loggers;
        private static ILoggerFactory _loggerFactory;

        static LogManager()
        {
            Loggers = new ConcurrentDictionary<string, ILogger>();
        }
        public static void SetLoggerFactory(ILoggerFactory factory)
        {
            _loggerFactory = factory;
        }

        public static ILogger GetLogger(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));
            if (_loggerFactory == null)
                return null;

            ILogger logger;
            if (Loggers.TryGetValue(name, out logger))
            {
                return logger;
            }
            logger = _loggerFactory.Create(name);
            Loggers[name] = logger;
            return logger;
        }

        public static ILogger GetLogger<T>()
        {
            if (_loggerFactory == null)
                return null;
            ILogger logger;
            var name = typeof (T).FullName;
            if (Loggers.TryGetValue(name, out logger))
            {
                return logger;
            }
            logger = _loggerFactory.Create<T>();
            Loggers[name] = logger;
            return logger;
        }

        public static ILogger GetLogger(Type type)
        {
            if (type == null)
                throw new ArgumentException(nameof(type));
            if (_loggerFactory == null)
                return null;

            var name = type.FullName;
            ILogger logger;
            if (Loggers.TryGetValue(name, out logger))
            {
                return logger;
            }
            logger = _loggerFactory.Create(type);
            Loggers[name] = logger;
            return logger;
        }
    }
}
