using System;
using Moedelo.InfrastructureV2.Json.Attributes;

namespace Moedelo.InfrastructureV2.Logging.Attributes
{
    /// <summary>
    /// Маскировать (заменять на ***) реальное значение свойства при логировании
    /// Работает только для объектов, передаваемых в extraData
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public sealed class MaskValueAtLoggingAttribute : MaskValueAtJsonSerializationAttribute
    {
        public MaskValueAtLoggingAttribute() : base("***")
        {
        }

        public MaskValueAtLoggingAttribute(string maskValue) : base(maskValue)
        {
        }
    }
}
