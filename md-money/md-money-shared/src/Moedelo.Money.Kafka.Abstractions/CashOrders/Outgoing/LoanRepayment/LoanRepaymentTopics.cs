namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class CashOrders
        {
            public static class LoanRepayment
            {
                public const string EntityName = nameof(LoanRepayment);

                public static class Event
                {
                    public static readonly string Topic = $"{Topics.MoneyTopics.Domain}.{Topics.MoneyTopics.CashOrders.Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
}
