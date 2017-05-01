using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ixq.Core.Entity;

namespace Ixq.UI.ComponentModel
{
    public class PropertyEditModel : IPropertyEditModel
    {
        public PropertyEditModel() { }

        public PropertyEditModel(IRuntimePropertyMenberInfo runtimeProperty, object entityDto)
        {
            this.RuntimeProperty = runtimeProperty;
            this.EntityDto = entityDto;
        }

        public IRuntimePropertyMenberInfo RuntimeProperty { get; set; }
        public object EntityDto { get; set; }

        public object Value
        {
            get { return RuntimeProperty.PropertyInfo.GetValue(EntityDto); }
        }
    }
}
