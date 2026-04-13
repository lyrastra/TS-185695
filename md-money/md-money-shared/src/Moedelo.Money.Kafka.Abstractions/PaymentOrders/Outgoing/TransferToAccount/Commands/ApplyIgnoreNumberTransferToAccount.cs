using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.TransferToAccount.Commands
{
    public class ApplyIgnoreNumberTransferToAccount : IEntityCommandData
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
