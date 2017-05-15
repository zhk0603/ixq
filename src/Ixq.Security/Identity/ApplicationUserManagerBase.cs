using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public abstract class ApplicationUserManagerBase<TUser> : UserManager<TUser>, IScopeDependency
        where TUser : class, IUser<string>
    {
        protected ApplicationUserManagerBase(IUserStore<TUser> store) : base(store)
        {
        }

        public virtual async Task<bool> UserExistsAsync(string userName)
        {
            var user = await base.FindByNameAsync(userName);
            return user != null;
        }
    }
}