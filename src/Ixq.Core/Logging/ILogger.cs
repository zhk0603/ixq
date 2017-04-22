using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Logging
{
    /// <summary>
    /// 日志接口。
    /// </summary>
    public interface ILogger
    {
        bool IsWarnEnabled { get; }
        bool IsFatalEnabled { get; }
        bool IsErrorEnabled { get; }
        bool IsDebugEnabled { get; }
        bool IsInfoEnabled { get; }

        /// <summary>
        /// 输入日志。
        /// </summary>
        /// <param name="message">日志消息。</param>
        /// <param name="exception">日志异常。</param>
        /// <param name="logLevel">日志级别。</param>
        void Write(string message, Exception exception, LogLevel logLevel);
    }
}
