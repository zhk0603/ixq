using System;
using System.ComponentModel.DataAnnotations;
using Ixq.Core.Entity;

namespace Ixq.Demo.Entities
{
    public abstract class EntityBase : EntityBase<Guid>
    {
        [StringLength(200)]
        public virtual string SortCode { get; set; }
    }

    public abstract class EntityBase<TKey> :IEntity<TKey>, ICreateSpecification, IUpdataSpecification,
        ISoftDeleteSpecification
    {
        [Key]
        public TKey Id { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        public DateTime? UpdataDate { get; set; }
        public DateTime? DeleteDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual void OnCreateComplete()
        {
            CreateDate = DateTime.Now;
        }
        public virtual void OnUpdataComplete()
        {
        }
        public virtual void OnSoftDeleteComplete()
        {
        }
    }
}