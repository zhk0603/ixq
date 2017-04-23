using Ixq.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Security.Identity;

namespace Ixq.Demo.Entities
{
    public class ApplicationRole : IdentityRoleBase
    {
        public string Description { get; set; }
    }
}
