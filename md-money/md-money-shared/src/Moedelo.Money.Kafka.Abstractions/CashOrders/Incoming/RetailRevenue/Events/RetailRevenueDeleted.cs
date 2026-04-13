using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.RetailRevenue.Events
{
    public class RetailRevenueDeleted : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }
    }
}
