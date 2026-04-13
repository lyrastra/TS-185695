using Moedelo.Accounting.Enums;

namespace Moedelo.Accounting.Kafka.Abstractions.Events.EventData.ClosedPeriods
{
    /// <summary>
    /// Отрицательный остаток по Бух счету
    /// </summary>
    public class NegativeBalance
    {
        /// <summary>
        /// Бух счет
        /// </summary>
        public SyntheticAccountCode Code { get; set; }

        /// <summary>
        /// Сумма остатка
        /// </summary>
        public decimal Sum { get; set; }
    }
}