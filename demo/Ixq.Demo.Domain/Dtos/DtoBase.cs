using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Demo.Domain.Dtos
{
    public abstract class DtoBase<TEntity> : Core.Dto.DtoBase<TEntity>
    {
        [Key]
        [Required]
        public override Guid Id { get; set; }
    }
}
