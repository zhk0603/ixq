using Ixq.Core.Dto;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Domain.Dtos
{
    public class ProductTypeDot : DtoBase<ProductType>
    {
        public string Name { get; set; }

        public ProductType ParentType { get; set; }
    }
}