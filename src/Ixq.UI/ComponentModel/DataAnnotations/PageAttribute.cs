using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.UI.Controls;

namespace Ixq.UI.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class PageAttribute : Attribute, IPageConfig
    {
        /// <summary>
        ///     表格名称。
        /// </summary>
        public string TitleName { get; set; }
        /// <summary>
        ///     默认排序字段名称。
        /// </summary>
        public string DefaultSortname { get; set; }
        /// <summary>
        /// Ajax请求数据的 Actin 名。
        /// </summary>
        public string DataActin { get; set; } = "List";

        /// <summary>
        /// 保存编辑、添加数据的  Actin 名。
        /// </summary>
        public string EditAction { get; set; } = "Edit";

        /// <summary>
        /// 删除数据的  Actin 名。
        /// </summary>
        public string DelAction { get; set; } = "Delete";

        /// <summary>
        /// 自定义Button。
        /// </summary>
        public Button[] ButtonCustom { get; set; }
    }
}
