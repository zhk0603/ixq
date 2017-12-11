using System;

namespace Ixq.Core.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAliasAttribute : Attribute
    {
        public ServiceAliasAttribute(string alias)
        {
            Alias = alias;
        }

        public string Alias { get; set; }
    }
}