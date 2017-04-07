using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    /// 编辑规范
    /// </summary>
    public interface IEditSpecification
    {
        DateTime? EditDate { get; set; }
        void OnEdtiComplete();
    }
}
