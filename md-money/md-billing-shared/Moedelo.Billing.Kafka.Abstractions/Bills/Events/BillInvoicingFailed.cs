using System;
using Moedelo.Billing.Shared.Enums.InvoiceBill;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Billing.Kafka.Abstractions.Bills.Events
{
    /// <summary>
    /// Событие "Ошибка выставления счёта"
    /// </summary>
    public class BillInvoicingFailed : IEntityEventData
    {
        /// <summary>
        /// Идентификатор команды
        /// </summary>
        public Guid RequestGuid { get; set; }

        public int FirmId { get; set; }

        public FailReason Reason { get; set; }

        public string Message { get; set; }
    }
}