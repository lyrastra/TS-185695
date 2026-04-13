namespace Moedelo.Money.Kafka.Abstractions.Topics
{
    public partial class MoneyTopics
    {
        public static partial class PaymentOrders
        {
            /// <summary>
            /// Общие события по всем операция по р/сч
            /// </summary>
            public static class Operation
            {
                public const string EntityName = nameof(Operation);

                public static class Event
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Event.{EntityName}";
                }
            }

            /// <summary>
            /// Массовое проведение операций по р/сч
            /// </summary>
            public static class BatchProvide
            {
                public const string EntityName = nameof(BatchProvide);

                public static class Command
                {
                    public static readonly string Topic = $"{Domain}.{Subdomain}.Command.{EntityName}";
                }
            }
        }
    }
}
