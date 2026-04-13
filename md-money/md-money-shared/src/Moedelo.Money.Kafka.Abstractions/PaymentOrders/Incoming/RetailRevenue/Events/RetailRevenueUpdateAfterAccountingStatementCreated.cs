using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.RetailRevenue.Events
{
    public class RetailRevenueUpdateAfterAccountingStatementCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public long AccountingStatementBaseId { get; set; }

        public DateTime AccountingStatementDate { get; set; }

        public decimal AccountingStatementSum { get; set; }
    }
}
