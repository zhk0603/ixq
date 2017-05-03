using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Data.DataAnnotations
{

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class PropertyAuthorizationAttribute : Attribute
    {
        public string[] Roles { get; set; }
        public string[] Users { get; set; }

        public virtual bool IsAuthorization(IPrincipal user)
        {
            if (user == null || user.Identity == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            if (Users != null && Users.Any() && !Users.Contains(user.Identity.Name, StringComparer.OrdinalIgnoreCase))
            {
                return false;
            }
            if (Roles != null && !Roles.Any(user.IsInRole))
            {
                return false;
            }
            return true;
        }
    }
}
