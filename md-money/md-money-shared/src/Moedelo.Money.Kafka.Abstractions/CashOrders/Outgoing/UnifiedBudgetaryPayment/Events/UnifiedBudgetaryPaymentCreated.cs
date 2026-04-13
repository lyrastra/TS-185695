using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events.Models;
using System;
using System.Collections.Generic;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.UnifiedBudgetaryPayment.Events
{
    public class UnifiedBudgetaryPaymentCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public IReadOnlyCollection<UnifiedBudgetarySubPayment> SubPayments { get; set; }

        /// <summary>
        /// Получатель платежа
        /// </summary>
        public string Recipient { get; set; }

        public string Destination { get; set; }
    }
}
