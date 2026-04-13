using System;
using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Incoming.MiddlemanRetailRevenue.Events
{
    public class MiddlemanRetailRevenueDeleted : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int KontragentId { get; set; }
    }
}
