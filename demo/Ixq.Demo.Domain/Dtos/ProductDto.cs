using Ixq.Core.Dto;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Domain.Dtos
{
    public class ProductDto : DtoBase<Product>
    {
        public ProductType Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}