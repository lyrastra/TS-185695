namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.ContributionOfOwnFunds
{
    public static class ContributionOfOwnFundsConstants
    {
        public const string EntityName = "ContributionOfOwnFunds";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
