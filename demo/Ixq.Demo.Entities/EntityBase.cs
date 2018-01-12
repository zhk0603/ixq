using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ixq.Core.Entity;

namespace Ixq.Demo.Entities
{
    public abstract class EntityBase : EntityBase<Guid>
    {
        [StringLength(200)]
        public virtual string SortCode { get; set; }
    }

    public abstract class EntityBaseInt32 : EntityBase<int>
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override int Id { get; set; }

        [StringLength(200)]
        public virtual string SortCode { get; set; }
    }

    public abstract class EntityBase<TKey> :IEntity<TKey>, ICreateSpecification, IUpdateSpecification,
        ISoftDeleteSpecification
    {
        [Key]
        public virtual TKey Id { get; set; }
        [Required]
        public virtual DateTime CreateDate { get; set; }

        public string CreateUserId { get; set; }
        public virtual DateTime? UpdataDate { get; set; }
        public string UpdateUserId { get; set; }
        public virtual DateTime? DeleteDate { get; set; }
        public string DeleteUserId { get; set; }
        public virtual bool IsDeleted { get; set; }
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