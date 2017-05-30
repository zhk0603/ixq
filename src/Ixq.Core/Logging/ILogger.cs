using System;

namespace Ixq.Core.Logging
{
    /// <summary>
    ///     日志接口。
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     是否启用了<see cref="LogLevel.Warn" />
        /// </summary>
        bool IsWarnEnabled { get; }

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Fatal" />
        /// </summary>
        bool IsFatalEnabled { get; }

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Error" />
        /// </summary>
        bool IsErrorEnabled { get; }

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Debug" />
        /// </summary>
        bool IsDebugEnabled { get; }

        /// <summary>
        ///     是否启用了<see cref="LogLevel.Info" />
        /// </summary>
        bool IsInfoEnabled { get; }

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        void Debug(string message);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        void Debug(Func<string> func);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Debug(string message, Exception exception);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        void Error(string message);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        void Error(Func<string> func);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Error(string message, Exception exception);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string message);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        void Fatal(Func<string> func);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Fatal(string message, Exception exception);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        void Info(string message);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        void Info(Func<string> func);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Info(string message, Exception exception);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        void Warn(string message);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="func"></param>
        void Warn(Func<string> func);

        /// <summary>
        ///     记录日志。
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        void Warn(string message, Exception exception);
    }
}