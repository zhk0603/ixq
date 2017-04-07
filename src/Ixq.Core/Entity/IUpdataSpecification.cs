using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    /// 更新规范
    /// </summary>
    public interface IUpdataSpecification
    {
        DateTime? UpdataDate { get; set; }
        void OnUpdataComplete();
    }
}
