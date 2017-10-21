using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Ixq.Demo.Entities
{
    public class ApplicationRole : IdentityRole, ICreateSpecification, IUpdataSpecification,
        ISoftDeleteSpecification
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string name) : base(name) { }
        public string Description { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdataDate { get; set; }
        public DateTime? DeleteDate { get; set; }
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
