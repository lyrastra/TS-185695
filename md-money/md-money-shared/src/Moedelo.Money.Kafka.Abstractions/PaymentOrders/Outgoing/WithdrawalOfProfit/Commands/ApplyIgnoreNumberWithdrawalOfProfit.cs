using Moedelo.Common.Kafka.Abstractions.Entities.Commands;

namespace Moedelo.Money.Kafka.Abstractions.PaymentOrders.Outgoing.WithdrawalOfProfit.Commands
{
    public class ApplyIgnoreNumberWithdrawalOfProfit : IEntityCommandData
    {
        public long[] DocumentBaseIds { get; set; }

        public int ImportRuleId { get; set; }
    }
}
