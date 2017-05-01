using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Ixq.Core.Entity;
using Ixq.UI.ComponentModel;
using Ixq.UI.Controls;

namespace Ixq.UI
{
    public static class PageExtensions
    {
        public static MvcHtmlString PropertyViewer(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyViewModel();
            if (runtimeProperty.DataType == Core.DataType.CustomDataType)
                return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.CustomDataType + "Viewer", model);

            return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.DataType + "Viewer", model);

        }

        public static MvcHtmlString PropertyEditor(this HtmlHelper helper, IRuntimePropertyMenberInfo runtimeProperty,
            object entityDto)
        {
            if (helper == null)
                throw new ArgumentNullException(nameof(helper));
            if (runtimeProperty == null)
                throw new ArgumentNullException(nameof(runtimeProperty));
            if (entityDto == null)
                throw new ArgumentNullException(nameof(entityDto));

            var model = new PropertyEditModel(runtimeProperty, entityDto);
            if (runtimeProperty.DataType == Core.DataType.CustomDataType)
                return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.CustomDataType + "Editor", model);

            return helper.Partial(runtimeProperty.PartialViewPath + runtimeProperty.DataType + "Editor", model);
        }

        public static MvcHtmlString ButtonCustom(this HtmlHelper helper, Button[] customButton)
        {
            return helper.Partial("");
        }
    }
}
