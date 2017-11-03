using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ixq.Core.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAliasAttribute : Attribute
    {
        public string Alias { get; set; }

        public ServiceAliasAttribute(string alias)
        {
            Alias = alias;
        }
    }
}
