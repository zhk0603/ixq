using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Ixq.Security.Identity
{
    public interface IAppUserStore<TUser> : IUserStore<TUser, long>
        where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
    {
    }
}
