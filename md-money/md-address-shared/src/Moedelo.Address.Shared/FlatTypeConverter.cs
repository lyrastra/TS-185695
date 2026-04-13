using System;
using System.Collections.Generic;
using System.Linq;

namespace Moedelo.Address.Shared
{
    public static class FlatTypeConverter
    {
        private static readonly Dictionary<string, string> TypeDictionary = new Dictionary<string, string>
        {
            { "Квартира", "кв." },
            { "Комната", "ком." },
            { "Офис", "оф." },
            { "Цех", "цех" },
            { "Блок", "блок" },
            { "Помещение", "помещ." }
        };

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
