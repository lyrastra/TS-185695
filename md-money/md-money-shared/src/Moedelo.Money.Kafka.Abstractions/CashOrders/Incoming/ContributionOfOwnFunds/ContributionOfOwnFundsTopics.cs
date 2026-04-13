namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class CashOrders
        {
            public static class ContributionOfOwnFunds
            {
                public const string EntityName = nameof(ContributionOfOwnFunds);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
    
}
