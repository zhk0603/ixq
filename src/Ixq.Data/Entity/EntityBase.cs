using System;
using System.ComponentModel.DataAnnotations;
using Ixq.Core.Entity;

namespace Ixq.Data.Entity
{
    public abstract class EntityBase : IEntity<Guid>
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}