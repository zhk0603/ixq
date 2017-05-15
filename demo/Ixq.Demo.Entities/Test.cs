using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Demo.Entities
{

    public class Test : EntityBaseInt32
    {
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Name1 { get; set; }

        [StringLength(200)]
        public string Name2 { get; set; }

        [StringLength(200)]
        public string Name3 { get; set; }

        [StringLength(200)]
        public string Name4 { get; set; }

        public bool? BoolTest { get; set; }
        public decimal? DecimalTest { get; set; }

        public int IntegerTest { get; set; }
            
    }
}
