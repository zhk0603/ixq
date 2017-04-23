using Ixq.Core.Cache;
using Ixq.Core.Logging;
using Ixq.Demo.Web;
using Ixq.Logging.Log4Net;
using Ixq.Owin.Extensions;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof (Startup))]

namespace Ixq.Demo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // 启用缓存
            ICacheProvider cacheProvider = new MemoryCacheProvider();
            CacheManager.SetCacheProvider(cacheProvider);

            // 启用日志
            ILoggerFactory factory = new Log4NetLoggerFactory();
            LogManager.SetLoggerFactory(factory);

            app.Initialization()
                .RegisterAutoMappe()
                .RegisterAutofac(typeof (MvcApplication));
        }
    }
}