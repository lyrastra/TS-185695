using System;

namespace Moedelo.InfrastructureV2.Json.Attributes
{
    public abstract class MaskValueAtJsonSerializationAttribute : Attribute
    {
        public string MaskValue { get; private set; }

        protected MaskValueAtJsonSerializationAttribute(string maskValue)
        {
            MaskValue = maskValue;
        }
    }
}
