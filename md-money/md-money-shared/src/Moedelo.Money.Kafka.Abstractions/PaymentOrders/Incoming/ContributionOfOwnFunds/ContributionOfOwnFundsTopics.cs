namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public partial class MoneyTopics
    {
        public static partial class PaymentOrders
        {
            public static class ContributionOfOwnFunds
            {
                public const string EntityName = nameof(ContributionOfOwnFunds);

                public static class Command
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Command.{EntityName}";
                }
            }
        }
    }
}
