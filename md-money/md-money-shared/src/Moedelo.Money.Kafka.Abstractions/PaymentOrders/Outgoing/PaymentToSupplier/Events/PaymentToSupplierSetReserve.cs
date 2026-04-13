using Moedelo.Common.Kafka.Abstractions.Entities.Events;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Events
{
    public class PaymentToSupplierSetReserve : IEntityEventData
    {
        public long DocumentBaseId { get; set; }
        public decimal? ReserveSum { get; set; }
    }
}