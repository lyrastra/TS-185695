namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public partial class MoneyTopics
    {
        public static partial class CashOrders
        {
            public static class WithdrawalOfProfit
            {
                public const string EntityName = nameof(WithdrawalOfProfit);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
}
