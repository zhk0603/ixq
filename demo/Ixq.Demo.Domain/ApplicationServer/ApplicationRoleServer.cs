using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;

namespace Ixq.Demo.Domain.ApplicationServer
{
    public class ApplicationRoleServer : ApplicationRoleManagerBase<ApplicationRole>
    {
        public ApplicationRoleServer(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }
    }
}