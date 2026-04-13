using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.Other.Commands.Models
{
    /// <summary>
    /// Бухгалтерская проводка для использования в командах импорта прочего
    /// </summary>
    public class OutgoingOtherAccPosting
    {
        /// <summary>
        /// Код счета по дебету
        /// </summary>
        public int DebitCode { get; set; }

        /// <summary>
        /// Список дебетовых субконто
        /// </summary>
        public IReadOnlyCollection<Subconto> DebitSubcontos { get; set; }
        
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Description { get; set; }
    }
}
