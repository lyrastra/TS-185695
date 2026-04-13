using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.BudgetaryPayment.Events
{
    public class BudgetaryPaymentUpdateAfterAccountingStatementCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public long AccountingStatementBaseId { get; set; }

        public DateTime AccountingStatementDate { get; set; }

        public decimal AccountingStatementSum { get; set; }
    }
}
