using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ixq.Core.Entity
{
    /// <summary>
    ///     实体基类。
    /// </summary>
    public abstract class EntityPrimaryInt : IEntity<int>
    {
        /// <summary>
        ///     获取或设置主键。
        /// </summary>
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }
}