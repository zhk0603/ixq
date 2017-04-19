using Ixq.Demo.Web;
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
            app.Initialization()
                .RegisterAutoMappe()
                .RegisterAutofac(typeof (MvcApplication));
        }
    }
}