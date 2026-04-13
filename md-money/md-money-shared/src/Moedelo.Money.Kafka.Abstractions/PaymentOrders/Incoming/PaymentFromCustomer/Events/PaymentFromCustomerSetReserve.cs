using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Incoming.PaymentFromCustomer.Events
{
    public class PaymentFromCustomerSetReserve : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
        public decimal? ReserveSum { get; set; }
    }
}