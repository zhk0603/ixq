using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ixq.Core.Dto;
using Ixq.Core.Mapper;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Web.Models
{
    public class ProductDto : DtoBase<Product>
    {
        public Guid Type { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public ProductDto(IMapper mapper) : base(mapper)
        {
        }
    }
}