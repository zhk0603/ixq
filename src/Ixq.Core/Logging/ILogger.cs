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

        void Debug(string message);
        void Debug(Func<string> func);
        void Debug(string message, Exception exception);
        void Error(string message);
        void Error(Func<string> func);
        void Error(string message, Exception exception);
        void Fatal(string message);
        void Fatal(Func<string> func);
        void Fatal(string message, Exception exception);
        void Info(string message);
        void Info(Func<string> func);
        void Info(string message, Exception exception);
        void Warn(string message);
        void Warn(Func<string> func);
        void Warn(string message, Exception exception);
    }
}
