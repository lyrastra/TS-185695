using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace Moedelo.BankIntegrations.Enums.Extensions
{
    public static class EnumExtensions
    {
        public static TEnum Parse<TEnum>(this Enum e) where TEnum : Enum
            => (TEnum)Enum.Parse(typeof(TEnum), e.ToString());

        public static TEnum ToEnum<TEnum>(this int v) where TEnum : Enum
            => (TEnum)Enum.Parse(typeof(TEnum), v.ToString());

        public static string ToEnumMemberString(this Enum enumValue)
        {
            if (enumValue == null)
                throw new ArgumentNullException(nameof(enumValue));

            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attribute = memberInfo[0].GetCustomAttribute<EnumMemberAttribute>();
                if (attribute != null && !string.IsNullOrEmpty(attribute.Value))
                {
                    return attribute.Value;
                }
            }

            return enumValue.ToString();
        }
    }
}
