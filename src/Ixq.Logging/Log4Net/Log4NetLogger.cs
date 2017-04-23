using System;
using Ixq.Core.Logging;
using log4net;
using log4net.Core;
using ILogger = Ixq.Core.Logging.ILogger;

namespace Ixq.Logging.Log4Net
{
    /// <summary>
    ///     Log4Net。
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        private static readonly Type DeclaringType = typeof (Log4NetLogger);
        private readonly log4net.Core.ILogger _logger;

        public Log4NetLogger(log4net.Core.ILogger logger)
        {
            _logger = logger;
        }

        public Log4NetLogger(ILog log) : this(log.Logger)
        {
        }

        public bool IsDebugEnabled => _logger.IsEnabledFor(Level.Debug);

        public bool IsErrorEnabled => _logger.IsEnabledFor(Level.Error);

        public bool IsFatalEnabled => _logger.IsEnabledFor(Level.Fatal);

        public bool IsInfoEnabled => _logger.IsEnabledFor(Level.Info);

        public bool IsWarnEnabled => _logger.IsEnabledFor(Level.Warn);

        public void Debug(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Debug);
        }

        public void Debug(string message)
        {
            Write(message, null, LogLevel.Debug);
        }

        public void Debug(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Debug);
        }

        public void Error(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Error);
        }

        public void Error(string message)
        {
            Write(message, null, LogLevel.Error);
        }

        public void Error(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Error);
        }

        public void Fatal(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Fatal);
        }

        public void Fatal(string message)
        {
            Write(message, null, LogLevel.Fatal);
        }

        public void Fatal(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Fatal);
        }

        public void Info(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Info);

        }

        public void Info(string message)
        {
            Write(message, null, LogLevel.Info);
        }

        public void Info(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Info);
        }

        public void Warn(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Warn);
        }

        public void Warn(string message)
        {
            Write(message, null, LogLevel.Warn);
        }

        public void Warn(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Warn);
        }

        protected void Write(string message, Exception exception, LogLevel logLevel)
        {
            if (_logger == null)
                throw new ArgumentException("Logger");

            switch (logLevel)
            {
                case LogLevel.Debug:
                    if (IsDebugEnabled)
                    {
                        _logger.Log(DeclaringType, Level.Debug, message, exception);
                    }
                    break;
                case LogLevel.Error:
                    if (IsErrorEnabled)
                    {
                        _logger.Log(DeclaringType, Level.Error, message, exception);
                    }
                    break;
                case LogLevel.Fatal:
                    if (IsFatalEnabled)
                    {
                        _logger.Log(DeclaringType, Level.Fatal, message, exception);
                    }
                    break;
                case LogLevel.Info:
                    if (IsInfoEnabled)
                    {
                        _logger.Log(DeclaringType, Level.Info, message, exception);
                    }
                    break;
                case LogLevel.Warn:
                    if (IsWarnEnabled)
                    {
                        _logger.Log(DeclaringType, Level.Warn, message, exception);
                    }
                    break;
            }
        }
    }
}