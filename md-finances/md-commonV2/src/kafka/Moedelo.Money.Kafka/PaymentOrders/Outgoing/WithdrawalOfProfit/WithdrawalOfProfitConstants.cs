namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.WithdrawalOfProfit
{
    public static class WithdrawalOfProfitConstants
    {
        public const string EntityName = "WithdrawalOfProfit";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
