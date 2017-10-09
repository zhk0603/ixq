using Ixq.Core.Entity;

namespace Ixq.Security.Identity
{
    public interface IRole : Microsoft.AspNet.Identity.IRole, IEntity<string>
    {
        new string Id { get; set; }
    }
}