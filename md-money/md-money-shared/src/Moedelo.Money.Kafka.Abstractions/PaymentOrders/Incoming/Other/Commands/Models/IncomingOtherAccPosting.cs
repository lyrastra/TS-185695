using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.Other.Commands.Models
{
    /// <summary>
    /// Бухгалтерская проводка для использования в командах импорта прочего
    /// </summary>
    public class IncomingOtherAccPosting
    {
        /// <summary>
        /// Код счета по кредиту
        /// </summary>
        public int CreditCode { get; set; }

        /// <summary>
        /// Список кредитовых субконто
        /// </summary>
        public IReadOnlyCollection<Subconto> CreditSubcontos { get; set; }

        /// <summary>
        /// Комментарий
        /// </summary>
        public string Description { get; set; }
    }
}
