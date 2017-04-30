using System;
using System.ComponentModel.DataAnnotations;

namespace Ixq.Core.Entity
{
    public abstract class EntityPrimaryGuid : IEntity<Guid>
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}