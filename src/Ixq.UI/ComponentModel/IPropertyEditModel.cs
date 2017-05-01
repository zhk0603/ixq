using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    public interface IPropertyEditModel
    {
        IRuntimePropertyMenberInfo RuntimeProperty { get; set; }
        object EntityDto { get; set; }
        object Value { get; }
    }
}
