using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    public interface IRuntimeEntityMenberInfo
    {
        IRuntimePropertyMenberInfo[] ViewPropertyInfo { get; }
        IRuntimePropertyMenberInfo[] CreatePropertyInfo { get; }
        IRuntimePropertyMenberInfo[] EditPropertyInfo { get; }
        IRuntimePropertyMenberInfo[] DetailPropertyInfo { get; }
        IRuntimePropertyMenberInfo[] SearcherPropertyInfo { get; }
    }
}
