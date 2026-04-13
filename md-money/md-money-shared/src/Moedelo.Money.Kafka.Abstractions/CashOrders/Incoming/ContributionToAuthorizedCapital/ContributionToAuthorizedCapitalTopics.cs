namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class CashOrders
        {
            public static class ContributionToAuthorizedCapital
            {
                public const string EntityName = nameof(ContributionToAuthorizedCapital);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
    
}
