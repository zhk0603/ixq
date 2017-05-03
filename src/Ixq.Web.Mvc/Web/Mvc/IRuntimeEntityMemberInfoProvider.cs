using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.DependencyInjection;
using Ixq.Core.Entity;

namespace Ixq.Web.Mvc
{
    public interface IRuntimeEntityMemberInfoProvider : IScopeDependency
    {
        IRuntimeEntityMenberInfo GetRuntimeEntityMenberInfo(Type type, IPrincipal user);
        IRuntimeEntityMenberInfo GetRuntimeEntityMenberInfo<T>(IPrincipal user);
    }
}
