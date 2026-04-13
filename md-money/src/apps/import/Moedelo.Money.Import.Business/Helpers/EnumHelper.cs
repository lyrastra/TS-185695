using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Moedelo.Money.Import.Business.Helpers
{
    public static class EnumHelper
    {
        public static IEnumerable<T> EnumToList<T>()
        {
            var enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException("T must be of type System.Enum");

            var enumValArray = Enum.GetValues(enumType);
            var enumValList = new List<T>(enumValArray.Length);
            enumValList.AddRange(from int val in enumValArray select (T)Enum.Parse(enumType, val.ToString()));
            return enumValList;
        }
        
        public static string GetEnumDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }
    }
}