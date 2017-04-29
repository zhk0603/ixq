using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Data.DataAnnotations;

namespace Ixq.Demo.Domain.Dtos
{
    public abstract class DtoBase<TEntity> : Core.Dto.DtoBase<TEntity>
    {
        [Key]
        [Required]
        [Hide(IsHiddenOnCreate = true, IsHiddenOnDetail = true, IsHiddenOnEdit = true, IsHiddenOnView = false)]
        [Display(Name = "唯一标识",Order = 0)]
        public override Guid Id { get; set; }
    }
}
