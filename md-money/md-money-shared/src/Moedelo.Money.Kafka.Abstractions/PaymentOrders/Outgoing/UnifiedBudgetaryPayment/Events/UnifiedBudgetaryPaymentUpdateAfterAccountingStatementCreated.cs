using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.UnifiedBudgetaryPayment.Events
{
    public class UnifiedBudgetaryPaymentUpdateAfterAccountingStatementCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public long AccountingStatementBaseId { get; set; }

        public DateTime AccountingStatementDate { get; set; }

        public decimal AccountingStatementSum { get; set; }
    }
}
