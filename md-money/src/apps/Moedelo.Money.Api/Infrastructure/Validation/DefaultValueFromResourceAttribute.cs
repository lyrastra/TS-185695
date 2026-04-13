using System;
using System.ComponentModel;
using System.Resources;

namespace Moedelo.Money.Api.Infrastructure.Validation
{
    public class DefaultValueFromResourceAttribute : DefaultValueAttribute
    {
        private readonly Type resourceType;
        private readonly string resourceName;

        public DefaultValueFromResourceAttribute(Type resourceType, string resourceName)
            : base(null)
        {
            this.resourceType = resourceType;
            this.resourceName = resourceName;
        }

        public override object Value => GetResourceLookup(resourceType, resourceName);

        private static string GetResourceLookup(Type resourceType, string resourceName)
        {
            var rm = new ResourceManager(resourceType.FullName, resourceType.Assembly);
            return rm.GetString(resourceName);
        }
    }
}
