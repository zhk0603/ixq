using System;
using Ixq.Core.Entity;

namespace Ixq.Demo.Entities
{
    public abstract class EntityBase : Data.Entity.EntityBase, ICreateSpecification, IUpdataSpecification,
        ISoftDeleteSpecification
    {
        public DateTime CreateDate { get; set; }

        public void OnCreateComplete()
        {
            CreateDate = DateTime.Now;
        }

        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }

        public void OnSoftDeleteComplete()
        {
        }

        public DateTime? UpdataDate { get; set; }

        public void OnUpdataComplete()
        {
        }
    }
}