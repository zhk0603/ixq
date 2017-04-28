using System.ComponentModel.DataAnnotations;
using Ixq.Core.Dto;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Domain.Dtos
{
    public class ProductTypeDot : DtoBase<ProductType>
    {
        [Required(ErrorMessage = "产品类型名称不能为空。")]
        [Display(Name = "产品类型名称", Order = 0, Description = "", GroupName = "")]
        public string Name { get; set; }

        public ProductType ParentType { get; set; }
    }
}