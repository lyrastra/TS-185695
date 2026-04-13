using System;
using EasyNetQ;

namespace Moedelo.InfrastructureV2.EventBus
{
    public class DefaultAndLegacyTypeNameSerializer : ITypeNameSerializer
    {
        private readonly ITypeNameSerializer serializer = new DefaultTypeNameSerializer();
        private readonly ITypeNameSerializer serializerLegacy = new LegacyTypeNameSerializer();

        public string Serialize(Type type)
        {
            return serializerLegacy.Serialize(type);
        }

        public Type DeSerialize(string typeName)
        {
            return CustomDesiarializer(serializerLegacy, typeName)
                   ?? CustomDesiarializer(serializer, typeName);
        }

        private static Type CustomDesiarializer(ITypeNameSerializer ser, string typeName)
        {
            try
            {
                var type = ser.DeSerialize(typeName);
                return type;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}