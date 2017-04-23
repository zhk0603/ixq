using System;
using Ixq.Core.Entity;

namespace Ixq.Demo.Entities
{
    public abstract class EntityBase : EntityBase<Guid>
    {
    }

    public abstract class EntityBase<TKey> :IEntity<TKey>, ICreateSpecification, IUpdataSpecification,
        ISoftDeleteSpecification
    {
        public TKey Id { get; set; }
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