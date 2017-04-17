using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ixq.Core.Dto;
using Ixq.Core.Mapper;
using Ixq.Demo.Entities;

namespace Ixq.Demo.Web.Models
{
    public class ProductTypeDot : DtoBase<ProductType>
    {
        public string Name { get; set; }
        public Guid? ParentType { get; set; }
    }
}