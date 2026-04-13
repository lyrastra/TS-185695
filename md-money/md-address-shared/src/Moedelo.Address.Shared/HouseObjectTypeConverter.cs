using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Moedelo.Address.Shared
{
    public static class HouseObjectTypeConverter
    {
        private static readonly Dictionary<string, string> TypeDictionary = new Dictionary<string, string>
        {
            { "Домовладение", "дмвлд." },
            { "владение", "влд." },
            { "дом", "д." },
            { "корпус", "к." },
            { "строение", "стр." },
            { "Литер", "литер" },
            { "Гараж", "гараж" },
            { "Здание", "зд." },
            { "Подвал", "подв." },
            { "Сооружение", "соор." },
        };
    
        public static string GetWithShortType(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return null;
            }
    
            var result = value;
            foreach (var typePair in TypeDictionary)
            {
                var fullType = typePair.Key;
                var shortType = typePair.Value;

                if (result.ToLower().Contains(fullType.ToLower()))
                {
                    result = Regex.Replace(result, fullType, shortType, RegexOptions.IgnoreCase);
                }
            }
    
            return result;
        }
    
        public static string GetShortTypeByFullType(string fullType)
        {
            if (string.IsNullOrEmpty(fullType))
            {
                return null;
            }
    
            var shortType = TypeDictionary.SingleOrDefault(type =>
                string.Equals(type.Key, fullType, StringComparison.InvariantCultureIgnoreCase)).Value;
    
            return shortType ?? fullType;
        }

        public static string GetFullTypeByShortType(string shortType)
        {
            if (string.IsNullOrEmpty(shortType))
            {
                return null;
            }
    
            var fullType = TypeDictionary.SingleOrDefault(type =>
                string.Equals(type.Value.TrimEnd('.'), shortType.TrimEnd('.'), StringComparison.InvariantCultureIgnoreCase)).Key;
    
            return fullType ?? shortType;
        }
    }
}
