using Newtonsoft.Json.Serialization;

namespace Moedelo.InfrastructureV2.Json
{
    internal sealed class MaskedValueProvider : IValueProvider
    {
        private readonly string maskingValue;

        internal MaskedValueProvider(string maskingValue)
        {
            this.maskingValue = maskingValue;
        }

        public void SetValue(object target, object value)
        {
            throw new System.InvalidOperationException("Данный класс предназначен только для сериализации");
        }

        public object GetValue(object target)
        {
            return maskingValue;
        }
    }
}
