using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToSupplier.Commands
{
    public class ApplyIgnoreNumberPaymentToSupplier : IEntityCommandData
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
