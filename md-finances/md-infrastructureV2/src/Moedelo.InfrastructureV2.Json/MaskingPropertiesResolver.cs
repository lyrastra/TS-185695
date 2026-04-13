using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;
using Moedelo.InfrastructureV2.Json.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Moedelo.InfrastructureV2.Json
{
    internal sealed class MaskingPropertiesResolver : DefaultContractResolver
    {
        private const string DefaultMaskingValue = "***";
        private static readonly ConcurrentDictionary<string, MaskedValueProvider> MaskedValueProviders =
            new ConcurrentDictionary<string, MaskedValueProvider>();

        private readonly HashSet<string> ignoredProperties;
        private readonly bool maskByAttribute;

        public MaskingPropertiesResolver(IEnumerable<string> maskingProperties, bool maskByAttribute)
        {
            ignoredProperties = new HashSet<string>(maskingProperties, StringComparer.OrdinalIgnoreCase);
            this.maskByAttribute = maskByAttribute;
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            var masking = maskByAttribute ? member.GetCustomAttribute<MaskValueAtJsonSerializationAttribute>(true) : null;

            if (masking != null)
            {
                property.ValueProvider = MaskedValueProviders.GetOrAdd(masking.MaskValue, CreateMaskedValueProvider);
            }
            else if (ignoredProperties.Contains(property.PropertyName))
            {
                property.ValueProvider = MaskedValueProviders.GetOrAdd(DefaultMaskingValue, CreateMaskedValueProvider);
            }

            return property;
        }

        private static MaskedValueProvider CreateMaskedValueProvider(string maskingValue)
        {
            return new MaskedValueProvider(maskingValue);
        }
    }
}
