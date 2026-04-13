namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalFromAccount
{
    public static class WithdrawalFromAccountConstants
    {
        public const string EntityName = "WithdrawalFromAccount";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
