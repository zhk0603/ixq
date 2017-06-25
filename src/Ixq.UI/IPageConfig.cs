using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.UI.Controls;

namespace Ixq.UI
{
    public interface IPageConfig
    {
        string Title { get; set; }
        string DefaultSortname { get; set; }
        bool IsDescending { get; set; }
        string DataActin { get; set; }
        string EditAction { get; set; }
        string DelAction { get; set; }

        /// <summary>
        ///     获取或设置是否可多选。
        /// </summary>
        bool MultiSelect { get; set; }

        /// <summary>
        ///     获取或设置是否显示行号。
        /// </summary>
        bool ShowRowNumber { get; set; }

        /// <summary>
        ///     获取或设置加载完后执行的脚本方法。
        /// </summary>
        string OnLoadCompleteScript { get; set; }

        Button[] ButtonCustom { get; set; }
    }
}
