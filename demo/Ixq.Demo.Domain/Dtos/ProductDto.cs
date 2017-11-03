using System.ComponentModel.DataAnnotations;
using Ixq.Core.Dto;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Domain.Dtos
{
    public class ProductDto : DtoBase<Product>
    {
        public ProductType Type { get; set; }

        [Display( Name= "产品名称")]
        public string Name { get; set; }

        [Display( Name= "价格")]
        public decimal Price { get; set; }
    }
}