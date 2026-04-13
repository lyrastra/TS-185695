namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class BankBalanceHistory
        {
            public static class Movement
            {
                public const string EntityName = nameof(Movement);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
    
}
