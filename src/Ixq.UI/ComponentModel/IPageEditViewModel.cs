using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    public interface IPageEditViewModel
    {
        string Title { get; set; }
        object EntityDto { get; set; }
        IRuntimePropertyMenberInfo[] PropertyMenberInfo { get; set; }
    }
}
