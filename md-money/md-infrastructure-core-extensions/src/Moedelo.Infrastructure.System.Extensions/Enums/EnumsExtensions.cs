using System;
using System.ComponentModel;
using System.Linq;

namespace Moedelo.Infrastructure.System.Extensions.Enums
{
    public static class EnumsExtensions
    {
        public static string GetDescription<T>(this T theEnum)
        {
            var descriptionAttribute = theEnum.GetCustomAttribute<T, DescriptionAttribute>();
            return descriptionAttribute?.Description;
        }

        public static TAttribute GetCustomAttribute<T, TAttribute>(this T theEnum)
             where TAttribute : Attribute
        {
            return GetCustomAttributes<T, TAttribute>(theEnum).FirstOrDefault();
        }

        public static TAttribute[] GetCustomAttributes<T, TAttribute>(this T theEnum)
             where TAttribute : Attribute
        {
            var enumType = theEnum.GetType();
            var memberInfo = enumType.GetMember(theEnum.ToString()).FirstOrDefault();

            return memberInfo?.GetCustomAttributes(typeof(TAttribute), false)
                .Cast<TAttribute>()
                .ToArray() ?? Array.Empty<TAttribute>();
        }
    }
}