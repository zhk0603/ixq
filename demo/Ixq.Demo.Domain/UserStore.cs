using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Demo.Entities;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Domain
{
    public class UserStore<TUser> : UserStoreBase<TUser, ApplicationRole>
        where TUser : IdentityUser
    {
        public UserStore(DbContext context) : base(context)
        {
        }
    }
}
