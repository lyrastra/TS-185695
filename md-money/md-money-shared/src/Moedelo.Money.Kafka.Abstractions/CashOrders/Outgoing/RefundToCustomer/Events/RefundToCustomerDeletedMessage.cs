using System;

namespace Moedelo.Money.Kafka.Abstractions.CashOrders.Outgoing.RefundToCustomer.Events
{
    public class RefundToCustomerDeletedMessage
    {
        public long DocumentBaseId { get; set; }

        public DateTime Date { get; set; }

        public string Number { get; set; }

        public int KontragentId { get; set; }
    }
}