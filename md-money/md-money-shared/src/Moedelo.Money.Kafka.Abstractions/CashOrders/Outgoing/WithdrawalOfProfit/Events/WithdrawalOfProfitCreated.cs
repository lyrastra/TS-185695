using Moedelo.Common.Kafka.Abstractions.Entities.Events;
using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.WithdrawalOfProfit.Events
{
    public class WithdrawalOfProfitCreated : IEntityEventData
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public long CashId { get; set; }

        public decimal Sum { get; set; }

        public string Destination { get; set; }

        public string Comment { get; set; }
    }
}
