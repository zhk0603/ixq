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
        private static readonly Type DeclaringType = typeof(Log4NetLogger);
        private readonly log4net.Core.ILogger _logger;

        /// <summary>
        ///     使用<see cref="log4net.Core.ILogger" />初始化一个<see cref="Log4NetLogger" />
        /// </summary>
        /// <param name="logger"></param>
        public Log4NetLogger(log4net.Core.ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        ///     使用<see cref="ILog" />初始化一个<see cref="Log4NetLogger" />
        /// </summary>
        /// <param name="log"></param>
        public Log4NetLogger(ILog log) : this(log.Logger)
        {
        }

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Debug" />
        /// </summary>
        public bool IsDebugEnabled => _logger.IsEnabledFor(Level.Debug);

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Error" />
        /// </summary>
        public bool IsErrorEnabled => _logger.IsEnabledFor(Level.Error);

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Fatal" />
        /// </summary>
        public bool IsFatalEnabled => _logger.IsEnabledFor(Level.Fatal);

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Info" />
        /// </summary>
        public bool IsInfoEnabled => _logger.IsEnabledFor(Level.Info);

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Warn" />
        /// </summary>
        public bool IsWarnEnabled => _logger.IsEnabledFor(Level.Warn);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        public void Debug(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Debug);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string message)
        {
            Write(message, null, LogLevel.Debug);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Debug(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Debug);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="exception"></param>
        public void Debug(Exception exception)
        {
            Write(null, exception, LogLevel.Debug);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        public void Error(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Error);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Write(message, null, LogLevel.Error);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Error(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Error);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="exception"></param>
        public void Error(Exception exception)
        {
            Write(null, exception, LogLevel.Error);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        public void Fatal(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Fatal);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string message)
        {
            Write(message, null, LogLevel.Fatal);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Fatal(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Fatal);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="exception"></param>
        public void Fatal(Exception exception)
        {
            Write(null, exception, LogLevel.Fatal);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        public void Info(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Info);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Write(message, null, LogLevel.Info);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Info(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Info);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="exception"></param>
        public void Info(Exception exception)
        {
            Write(null, exception, LogLevel.Info);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        public void Warn(Func<string> func)
        {
            var message = func();
            Write(message, null, LogLevel.Warn);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            Write(message, null, LogLevel.Warn);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public void Warn(string message, Exception exception)
        {
            Write(message, exception, LogLevel.Warn);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="exception"></param>
        public void Warn(Exception exception)
        {
            Write(null, exception, LogLevel.Warn);
        }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message">日志信息。</param>
        /// <param name="exception">错误异常。</param>
        /// <param name="logLevel">日志级别。</param>
        protected virtual void Write(string message, Exception exception, LogLevel logLevel)
        {
            if (_logger == null)
                throw new ArgumentException("Logger");

            switch (logLevel)
            {
                case LogLevel.Debug:
                    if (IsDebugEnabled)
                        _logger.Log(DeclaringType, Level.Debug, message, exception);
                    break;
                case LogLevel.Error:
                    if (IsErrorEnabled)
                        _logger.Log(DeclaringType, Level.Error, message, exception);
                    break;
                case LogLevel.Fatal:
                    if (IsFatalEnabled)
                        _logger.Log(DeclaringType, Level.Fatal, message, exception);
                    break;
                case LogLevel.Info:
                    if (IsInfoEnabled)
                        _logger.Log(DeclaringType, Level.Info, message, exception);
                    break;
                case LogLevel.Warn:
                    if (IsWarnEnabled)
                        _logger.Log(DeclaringType, Level.Warn, message, exception);
                    break;
            }
        }
    }
}