using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Logging;
using log4net;
using log4net.Config;
using LogManager = log4net.LogManager;

namespace Ixq.Logging.Log4Net
{
    /// <summary>
    ///     Log4Net 日志工厂。
    /// </summary>
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        /// <summary>
        ///     初始化一个 <see cref="Log4NetLoggerFactory"/> 实例。
        /// </summary>
        /// <param name="configFileName">log4net 配置文件名字。</param>
        public Log4NetLoggerFactory(string configFileName)
        {
            var file = CreateConfigFileInfo(configFileName);
            XmlConfigurator.ConfigureAndWatch(file);
        }

        /// <summary>
        ///     初始化一个 <see cref="Log4NetLoggerFactory"/> 实例。
        /// </summary>
        public Log4NetLoggerFactory() : this("log4net.config")
        {
        }

        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public ILogger Create<T>()
        {
            return new Log4NetLogger(LogManager.GetLogger(typeof (T)));
        }

        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger Create(Type type)
        {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }

        /// <summary>
        ///     创建<see cref="ILogger" />。
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILogger Create(string name)
        {
            return new Log4NetLogger(LogManager.GetLogger(name));
        }

        private FileInfo CreateConfigFileInfo(string configFileName)
        {
            var filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, configFileName);
            
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new FileNotFoundException("Log4Net配置文件不存在。", filePath);
            }

            return fileInfo;
        }
    }
}
