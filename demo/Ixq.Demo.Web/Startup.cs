using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Ixq.Demo.Web.Startup))]
namespace Ixq.Demo.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
