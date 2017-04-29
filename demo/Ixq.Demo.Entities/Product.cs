using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ixq.Demo.Entities
{
    public class Product : EntityBase
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        [Required]
        public virtual ProductType Type { get; set; }
    }
}