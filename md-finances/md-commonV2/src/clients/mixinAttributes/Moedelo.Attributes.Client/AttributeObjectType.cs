using System;
using System.Linq;
using Moedelo.Common.Enums.Enums.mixinAttributes;

namespace Moedelo.Attributes.Client
{
    public struct AttributeObjectType
    {
        public byte Value { get; set; }

        #region byte

        public static implicit operator byte(AttributeObjectType d)
        {
            return d.Value;
        }

        public static implicit operator AttributeObjectType(byte d)
        {
            return new AttributeObjectType {Value = d};
        }

        #endregion

        #region AttributeObjectTypeEnum

        public static implicit operator AttributeObjectTypeEnum(AttributeObjectType d)
        {
            return (AttributeObjectTypeEnum) d.Value;
        }

        public static implicit operator AttributeObjectType(AttributeObjectTypeEnum d)
        {
            return (byte) d;
        }

        #endregion
    }
}