namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class CashOrders
        {
            public static class OtherOutgoing
            {
                public const string EntityName = nameof(OtherOutgoing);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
    
}
