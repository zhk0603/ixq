using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Demo.Entities.System
{
    /// <summary>
    ///     系统菜单
    /// </summary>
    public class SystemMenu : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string CssClass { get; set; }
        public string CssStyles { get; set; }
        public string Link { get; set; }
        public string Js { get; set; }
    }
}
