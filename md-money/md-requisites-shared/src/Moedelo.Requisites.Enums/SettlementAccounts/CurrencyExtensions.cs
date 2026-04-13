using System;
using System.Linq;

namespace Moedelo.Requisites.Enums.SettlementAccounts
{
    public static class CurrencyExtensions
    {
        private static TAttribute GetAttribute<TAttribute>(this Enum value)
            where TAttribute : Attribute
        {
            var enumType = value.GetType();
            var name = Enum.GetName(enumType, value);
            return enumType.GetField(name).GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
        }
        
        public static string ToCurrencySymbol(this Currency currency)
        {
            return currency.GetAttribute<CurrencySymbolAttribute>().ToString();
        }
        
        public static string ToCurrencyStringCode(this Currency currency)
        {
            var enumType = currency.GetType();
            return Enum.GetName(enumType, currency);
        }
        
        public static string ToCurrencyRussianName(this Currency currency)
        {
            return currency.GetAttribute<CurrencyRussianNameAttribute>().ToString();
        }
        
        public static Currency? GetCurrency(this string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length < 8)
            {
                return null;
            }
            var code = number.Substring(5, 3);
            Enum.TryParse(code, out Currency result);
            if (Enum.IsDefined(typeof(Currency), result))
            {
                return result == Currency.RUR
                    ? Currency.RUB
                    : result;
            }
            return null;
        }

        public static bool IsCurrency(this string number, Currency currency)
        {
            var numberCurrency = number.GetCurrency();
            return numberCurrency.HasValue &&
                   numberCurrency == currency;
        }
        
        public static bool IsForeignCurrency(this string number)
        {
            var numberCurrency = number.GetCurrency();
            return numberCurrency.HasValue &&
                   numberCurrency != Currency.RUB &&
                   numberCurrency != Currency.RUR;
        }
    }
}