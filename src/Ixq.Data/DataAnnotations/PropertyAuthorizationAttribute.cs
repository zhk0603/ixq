using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Data.DataAnnotations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public class PropertyAuthorizationAttribute : Attribute
    {
        public string[] Roles { get; set; }
        public string[] Users { get; set; }


    }
}
