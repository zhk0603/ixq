using ixq.Demo.DbContext;
using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace Ixq.Demo.Domain
{
    public class ApplicationRoleManager : AppRoleManager<ApplicationRole>
    {
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            return new ApplicationRoleManager(new AppRoleStore<ApplicationRole>(context.Get<DataContext>()));
        }

        public ApplicationRoleManager(IRoleStore<ApplicationRole, long> store) : base(store)
        {
        }
    }
}