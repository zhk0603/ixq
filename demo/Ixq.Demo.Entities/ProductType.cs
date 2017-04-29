using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Ixq.Demo.Entities
{
    public class ProductType : EntityBase
    {
        public string Name { get; set; }
        //[ForeignKey("ParentType_Id")]
        [Required]
        public virtual ProductType ParentType { get; set; }
        //public Guid ParentType_Id { get; set; }
    }
}