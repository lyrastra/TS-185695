namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public partial class MoneyTopics
    {
        public static partial class PaymentOrders
        {
            public static class ContributionToAuthorizedCapital
            {
                public const string EntityName = nameof(ContributionToAuthorizedCapital);

                public static class Command
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Command.{EntityName}";
                }
            }
        }
    }
}
