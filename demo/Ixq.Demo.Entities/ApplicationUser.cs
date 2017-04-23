using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Security;
using Ixq.Security.Identity;

namespace Ixq.Demo.Entities
{
    public class ApplicationUser : IdentityUserBase
    {
        public int Age { get; set; }
    }
}
