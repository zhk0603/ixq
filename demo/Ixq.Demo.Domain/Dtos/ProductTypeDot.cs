using System;
using System.ComponentModel.DataAnnotations;
using Ixq.Core.Dto;
using Ixq.Data.DataAnnotations;
using Ixq.Demo.Entities;
using Ixq.UI.ComponentModel.DataAnnotations;

namespace Ixq.Demo.Domain.Dtos
{
    [Page(DefaultSortname = nameof(Id), Title = "产品分类", MultiSelect = true, MultiBoxOnly = true)]
    public class ProductTypeDot : DtoBase<ProductType>
    {
        [Display(Name = "创建时间", Order = 1)]
        [ColModel(Sortable = true)]
        [PropertyAuthorization(Roles = new[] {"Admin"})]
        public DateTime CreateDate { get; set; }

        [Required(ErrorMessage = "产品类型名称不能为空。")]
        [Display(Name = "产品类型名称", Order = 2)]
        [ColModel(Align = Core.TextAlign.Center, Sortable = true)]
        [PropertyAuthorization(Users = new[] {"zhaokun"})]
        public string Name { get; set; }

        public ProductType ParentType { get; set; }
    }
}