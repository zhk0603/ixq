using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Demo.Entities
{
    public abstract class EntityBase : Data.Entity.EntityBase, ICreateSpecification, IUpdataSpecification, ISoftDeleteSpecification
    {
        public DateTime CreateDate { get; set; }
        public DateTime? UpdataDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public void OnCreateComplete()
        {
            CreateDate = DateTime.Now;
        }
        public void OnUpdataComplete()
        {
        }
        public void OnSoftDeleteComplete()
        {
        }
    }
}
