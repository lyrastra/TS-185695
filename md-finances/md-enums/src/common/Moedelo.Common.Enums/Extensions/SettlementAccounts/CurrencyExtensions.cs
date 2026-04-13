using System;
using Moedelo.Common.Enums.Enums.SettlementAccounts;

namespace Moedelo.Common.Enums.Extensions.SettlementAccounts
{
    public static class CurrencyExtensions
    {
        [Obsolete("Use GetCurrency")]
        public static Currency ToCurrency(this string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length < 8)
            {
                return Currency.RUB;
            }
            var code = number.Substring(5, 3);
            Currency result;
            Enum.TryParse(code, out result);

            return !Enum.IsDefined(typeof(Currency), result) || result == Currency.RUR ? Currency.RUB : result;
        }

        public static Currency? GetCurrency(this string number)
        {
            if (string.IsNullOrEmpty(number) || number.Length < 8)
            {
                return null;
            }
            var code = number.Substring(5, 3);
            if(code == "000")
                return null;

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