using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.Core.Data
{
    public interface IDataAnnotations
    {
        void SetRuntimeProperty(IRuntimePropertyMenberInfo runtimeProperty);
    }
}
