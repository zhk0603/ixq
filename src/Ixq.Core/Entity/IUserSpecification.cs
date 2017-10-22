using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    public interface IUserSpecification
    {
        DateTime? LastSignInDate { get; set; }
        DateTime? LastSignOutDate { get; set; }
        void OnSignInComplete();
        void OnSignOutComplete();
    }
}
