using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core;
using Ixq.Core.Entity;
using Ixq.Security.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Entities
{
    public class ApplicationRole : AppIdentityRole, ICreateSpecification, IUpdateSpecification,
        ISoftDeleteSpecification
    {
        public ApplicationRole()
        {
        }

        public ApplicationRole(string name) : base(name)
        {
        }

        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public string CreateUserId { get; set; }
        public DateTime? UpdataDate { get; set; }
        public string UpdateUserId { get; set; }
        public DateTime? DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public bool IsDeleted { get; set; }

        public void OnCreateComplete()
        {
        }

        public void OnUpdataComplete()
        {
        }

        public void OnSoftDeleteComplete()
        {
        }
    }
}
