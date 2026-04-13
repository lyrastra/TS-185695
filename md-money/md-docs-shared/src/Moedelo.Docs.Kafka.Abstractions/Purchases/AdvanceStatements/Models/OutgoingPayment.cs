using System;

namespace Moedelo.Docs.Kafka.Abstractions.Purchases.AdvanceStatements.Models
{
    /// <summary>
    /// Платёжный документ, привязанный к авансовому отчёту
    /// </summary>
    public class OutgoingPayment
    {
        /// <summary>
        /// Идентификатор документа
        /// </summary>
        public long DocumentBaseId { get; set; }

        /// <summary>
        /// Дата документа
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Сумма
        /// </summary>
        public decimal Sum { get; set; }

        /// <summary>
        /// Сумма связи с АО
        /// </summary>
        public decimal LinkSum { get; set; }
    }
}
