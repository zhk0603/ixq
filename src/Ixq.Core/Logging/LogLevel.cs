namespace Ixq.Core.Logging
{
    /// <summary>
    ///     日志级别。
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        ///     输出所有级别的日志
        /// </summary>
        All = 0,

        /// <summary>
        ///     表示调试的日志级别
        /// </summary>
        Debug = 1,

        /// <summary>
        ///     表示消息的日志级别
        /// </summary>
        Info = 2,

        /// <summary>
        ///     表示警告的日志级别
        /// </summary>
        Warn = 3,

        /// <summary>
        ///     表示错误的日志级别
        /// </summary>
        Error = 4,

        /// <summary>
        ///     表示严重错误的日志级别
        /// </summary>
        Fatal = 5,

        /// <summary>
        ///     关闭所有日志，不输出日志
        /// </summary>
        Off = 6
    }
}