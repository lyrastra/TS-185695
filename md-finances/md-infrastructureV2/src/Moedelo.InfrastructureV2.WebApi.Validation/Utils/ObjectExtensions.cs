using System;

namespace Moedelo.InfrastructureV2.WebApi.Validation.Utils
{
    public static class ObjectExtensions
    {
        public static bool MatchValue(this object v1, object v2)
        {
            var type = v1?.GetType();
            if (type == null)
            {
                return v2 == null;
            }
            dynamic d1 = Convert.ChangeType(v1, type);
            dynamic d2 = Convert.ChangeType(v2, type);

            return d1 == d2;
        }

        public static object EnumToInt(this object value)
        {
            return (value?.GetType().IsEnum ?? false) ? (int)value : value;
        }
    }
}