using System;
using Moedelo.Infrastructure.Json;

namespace Moedelo.Changelog.Shared.Domain.Definitions
{
    public class MoneySum
    {
        public MoneySum()
        {
        }

        public MoneySum(decimal value, string currency)
        {
            // явно увеличиваем до 4 знаков после запятой
            Value = decimal.Round(1.0000m * value, 4);
            Currency = currency;
        }

        public static MoneySum InRubles(decimal value)
        {
            return new MoneySum(value, "RUB");
        }

        public decimal Value { get; private set; }

        public string Currency { get; private set; }

        /// <summary>
        /// вычисляемое поле для сохранения полной информации о значении с валютой
        /// </summary>
        public string ValueAndCurrency
        {
            get => FormattableString.Invariant($"{{\"Value\":{Value:F4},\"Currency\":\"{Currency}\"}}");
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    return;
                }

                var holder = value.FromJsonString<Holder>();

                // явно увеличиваем до 4 знаков после запятой
                Value = decimal.Round(1.0000m * holder.Value, 4);
                Currency = holder.Currency;
            }
        }

        private struct Holder
        {
            public decimal Value { get; set; }
            public string Currency { get; set; }
        }
    }
}
