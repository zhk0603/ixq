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
    public class ApplicationUser : AppIdentityUser, ICreateSpecification, IUpdateSpecification,
        ISoftDeleteSpecification, IUser<long>
    {
        public virtual int Age { get; set; }
        public virtual DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public virtual DateTime? UpdataDate { get; set; }
        public string UpdateUserId { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
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

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(AppUserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }
}
