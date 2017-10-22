using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity;

namespace Ixq.Demo.Entities
{
    public class ApplicationUser : AppIdentityUser, ICreateSpecification, IUpdataSpecification,
        ISoftDeleteSpecification
    {
        public virtual int Age { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public virtual DateTime? UpdataDate { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public virtual bool IsDeleted { get; set; }
        public void OnCreateComplete()
        {
        }
        public void OnUpdataComplete()
        {
        }
        public void OnSoftDeleteComplete()
        {
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
