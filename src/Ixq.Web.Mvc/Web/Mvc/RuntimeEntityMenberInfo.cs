using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Web.Mvc
{
    public class RuntimeEntityMenberInfo
    {
        public RuntimeEntityMenberInfo(Type entityType, IPrincipal user)
        {
            
        }
        public PropertyInfo[] DisplayViewPropertyInfo { get; set; }
        public PropertyInfo[] DisplayCreatePropertyInfo { get; set; }
        public PropertyInfo[] DisplayEditPropertyInfo { get; set; }
        public PropertyInfo[] DisplayDetailPropertyInfo { get; set; }
    }
}
