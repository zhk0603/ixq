using Ixq.Core.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Ixq.Logging.Log4Net
{
    /// <summary>
    /// Log4Net。
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private readonly log4net.Core.ILogger _logger;
        public Log4NetLogger(log4net.Core.ILogger logger)
        {
            _logger = logger;
        }

        public Log4NetLogger(ILog log):this(log.Logger)
        {
        }

        public bool IsDebugEnabled => _logger.IsEnabledFor(log4net.Core.Level.Debug);

        public bool IsErrorEnabled => _logger.IsEnabledFor(log4net.Core.Level.Error);

        public bool IsFatalEnabled => _logger.IsEnabledFor(log4net.Core.Level.Fatal);

        public bool IsInfoEnabled => _logger.IsEnabledFor(log4net.Core.Level.Info);

        public bool IsWarnEnabled => _logger.IsEnabledFor(log4net.Core.Level.Warn);

        public void Write(string message, Exception exception, LogLevel logLevel)
        {
            throw new NotImplementedException();
        }
    }
}
