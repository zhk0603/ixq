using System;
using System.ComponentModel.DataAnnotations;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体基类。
    /// </summary>
    public abstract class EntityPrimaryGuid : IEntity<Guid>
    {
        /// <summary>
        ///     获取或设置主键。
        /// </summary>
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}