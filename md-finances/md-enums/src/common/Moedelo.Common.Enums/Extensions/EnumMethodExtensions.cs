using System;
using System.ComponentModel;
using System.Linq;

namespace Moedelo.Common.Enums.Extensions
{
    public static class EnumMethodExtensions
    {
        public static T GetEnumAttribute<T, TE>(this TE theEnum)
        {
            var enumType = theEnum.GetType();
            var memberInfo = enumType.GetMember(theEnum.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                return (T)memberInfo.GetCustomAttributes(typeof(T), false).FirstOrDefault();
            }

            return default(T);
        }

        /// <summary>
        /// Gets an attribute on an enum field value
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute you want to retrieve</typeparam>
        /// <param name="enumVal">The enum value</param>
        /// <returns>The attribute of type T that exists on the enum value</returns>
        /// <example>string desc = myEnumVariable.GetAttribute<DescriptionAttribute>().Description;</example>
        /// <source>https://stackoverflow.com/questions/1799370/getting-attributes-of-enums-value</source>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumVal) where TAttribute : Attribute
        {
            var type = enumVal.GetType();
            var memInfo = type.GetMember(enumVal.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(TAttribute), false);
            return (attributes.Length > 0) ? (TAttribute)attributes[0] : null;
        }

        public static string GetDescription<T>(this T theEnum)
        {
            var enumType = theEnum.GetType();
            var memberInfo = enumType.GetMember(theEnum.ToString()).FirstOrDefault();

            if (memberInfo != null)
            {
                var description = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

                if (description != null)
                {
                    return description.Description;
                }
            }

            return null;
        }

        public static T GetValueFromDescription<T>(string description)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attribute != null)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            return default(T);
        }

        public static string GetName<T>(this T theEnum)
        {
            var enumType = theEnum.GetType();

            return Enum.GetName(enumType, theEnum);
        }
    }
}
