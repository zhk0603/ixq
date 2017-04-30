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
        string TitleName { get; set; }
        string DefaultSortname { get; set; }
        bool IsDescending { get; set; }
        string DataActin { get; set; }
        string EditAction { get; set; }
        string DelAction { get; set; }
        Button[] ButtonCustom { get; set; }
    }
}
