using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Security.Identity
{
    public class AppUserValidator<TUser> : UserValidator<TUser,long> where TUser : class, IUser<long>, Ixq.Core.Security.IUser<long>
    {
        public AppUserValidator(UserManager<TUser, long> manager) : base(manager)
        {
        }
    }
}
