using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Logging;
using log4net;
using log4net.Config;

namespace Ixq.Logging.Log4Net
{
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        public Log4NetLoggerFactory(string configFileName)
        {
            var file = CreateConfigFileInfo(configFileName);
            XmlConfigurator.ConfigureAndWatch(file);
        }

        public Log4NetLoggerFactory() : this("log4net.config")
        {
        }

        public ILogger Create<T>()
        {
            return new Log4NetLogger(LogManager.GetLogger(typeof (T)));
        }

        public ILogger Create(Type type)
        {
            return new Log4NetLogger(LogManager.GetLogger(type));
        }

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
