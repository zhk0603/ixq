using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Dto;
using Ixq.Data.DataAnnotations;
using Ixq.Demo.Entities;
using Ixq.UI.ComponentModel.DataAnnotations;

namespace Ixq.Demo.Domain.Dtos
{
    [Page(DefaultSortname = nameof(Id), Title = "Test", IsDescending = true,MultiSelect =true)]
    public class TestDto : DtoBaseInt32<Entities.Test>
    {
        [Key]
        [Required]
        [Hide(IsHiddenOnCreate = true, IsHiddenOnDetail = true, IsHiddenOnEdit = true, IsHiddenOnView = false)]
        [Display(Name = "唯一标识")]
        [ColModel(Sortable = true)]
        public override int Id { get; set; }

        [Required]
        [Display(Name = "创建时间", Order = 1)]
        [ColModel(Sortable = true)]
        [PropertyAuthorization(Roles = new[] {"Admin"})]
        public DateTime CreateDate { get; set; }

        [Display(Name = "Name")]
        [StringLength(200, MinimumLength = 2)]
        [Required]
        [ColModel(Sortable = true)]
        public string Name { get; set; }

        [StringLength(200)]
        [Display(Name = "Name1")]
        [ColModel(Sortable = true)]
        [Hide]
        public string Name1 { get; set; }

        [StringLength(200)]
        [Display(Name = "Name2")]
        [ColModel(Sortable = true)]
        [Hide]
        public string Name2 { get; set; }

        [StringLength(200)]
        [Display(Name = "Name3")]
        [ColModel(Sortable = true)]
        [Hide]
        public string Name3 { get; set; }

        [StringLength(200)]
        [Display(Name = "Name4")]
        [ColModel(Sortable = true)]
        [Hide]
        public string Name4 { get; set; }

        [Display(Name = "布尔类型测试[NullValue]")]
        [Required]
        [ColModel(Sortable = true)]
        public bool? BoolTest { get; set; }

        [Display(Name = "布尔类型测试[不为空类型]")]
        [Required]
        [ColModel(Sortable = true)]
        public bool BoolTestNotNull { get; set; }

        [Number(Max = 9999999, Min = 0)]
        [Display(Name = "Decimal 类型测试")]
        [Required]
        [ColModel(Sortable = true)]
        public decimal? DecimalTest { get; set; }


        [Number(Max = 9999999, Min = 0)]
        [Display(Name = "Integer 类型测试")]
        [Required]
        [ColModel(Sortable = true)]
        public int IntegerTest { get; set; }


        [Display(Name = "Enum 类型测试")]
        [Required]
        public TestEnum1 TestEnum1 { get; set; }
        [Display(Name = "Enum 类型测试(Flags)")]
        [Required]
        public TestEnum2 TestEnum2 { get; set; }


        [Display(Name = "TimeSpan 类型测试")]
        [Required]
        public TimeSpan TestTimeSpan { get; set; }

    }
}
