namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public static partial class MoneyTopics
    {
        public static partial class PaymentOrders
        {
            public static string Subdomain = "PaymentOrders";

            public static class Outgoing
            {
                public const string EntityName = nameof(Outgoing);

                public static class Commands
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Command.{EntityName}";
                }
            }
        }
    }
}
