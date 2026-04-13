using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalFromAccount.Commands
{
    public class ApplyIgnoreNumberWithdrawalFromAccount : IEntityCommandData
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
