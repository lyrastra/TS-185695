using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Events
{
    /// <summary>
    /// Событие "Счёт выставлен"
    /// </summary>
    public class BillInvoiced : IEntityEventData
    {
        /// <summary>
        /// Идентификатор команды
        /// </summary>
        public Guid RequestGuid { get; set; }

        /// <summary>
        /// Идентификатор фирмы 
        /// </summary>
        public int FirmId { get; set; }

        /// <summary>
        /// Номер счета 
        /// </summary>
        public string BillNumber { get; set; }
    }
}