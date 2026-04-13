using System;
using System.ComponentModel;
using System.Linq;

namespace Moedelo.Spam.ApiClient.Abastractions.Extensions
{
    public static class EnumMethodExtensions
    {
        public static string GetDescription<T>(this T theEnum) where T : Enum
        {
            var enumType = theEnum.GetType();
            var memberInfo = enumType.GetMember(theEnum.ToString()).FirstOrDefault();

            if (memberInfo == null)
            {
                return null;
            }

            var description = (DescriptionAttribute)memberInfo.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

            return description?.Description;
        }
    }
}