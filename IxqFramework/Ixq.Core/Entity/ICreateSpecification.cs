using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.Entity
{
    /// <summary>
    /// 创建规范
    /// </summary>
    public interface ICreateSpecification
    {
        DateTime CreateDate { get; set; }
        void OnCreateComplete();
    }
}
