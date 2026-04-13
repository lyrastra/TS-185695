using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.PaymentToAccountablePerson.Commands
{
    public class ApplyIgnoreNumberPaymentToAccountablePerson : IEntityCommandData
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
