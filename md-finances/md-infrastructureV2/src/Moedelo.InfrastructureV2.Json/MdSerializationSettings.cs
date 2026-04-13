using System.Collections.Generic;
using Moedelo.InfrastructureV2.Json.Attributes;

namespace Moedelo.InfrastructureV2.Json
{
    public struct MdSerializationSettings
    {
        public bool UseLocalDateTimeZone { get; set; }
        public MdSerializerNullHandling NullHandling { get; set; }

        /// <summary>
        /// Маскировать значения свойств, помеченных атрибутом <see cref="MaskValueAtJsonSerializationAttribute"/>
        /// </summary>
        public bool MaskPropertiesByAttribute { get; set; }
        
        /// <summary>
        /// Удалять значения свойств, отнесённых в список 
        /// </summary>
        public bool MaskGenericSensitiveProperties { get; set; }

        /// <summary>
        /// Список свойств, значения которых должны маскироваться при сериализации 
        /// </summary>
        public IReadOnlyCollection<string> MaskProperties { get; set; }

        public MdSerializerSettingsEnum GetMdSerializerSettingsEnum()
        {
            return UseLocalDateTimeZone
                    ? MdSerializerSettingsEnum.None
                    : MdSerializerSettingsEnum.DateTimeZoneHandlingLocal;
        }
    }
}
