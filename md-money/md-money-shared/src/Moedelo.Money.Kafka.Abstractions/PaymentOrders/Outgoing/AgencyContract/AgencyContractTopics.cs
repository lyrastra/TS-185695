namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public partial class MoneyTopics
    {
        public static partial class PaymentOrders
        {
            public static class AgencyContract
            {
                public const string EntityName = nameof(AgencyContract);

                public static class Command
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Command.{EntityName}";
                }

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }
        }
    }
}
