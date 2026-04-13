namespace Moedelo.Konragents.Kafka.Abstractions
{
    public partial class KontragentsTopics
    {
        public static class KontragentDebt
        {
            public const string EntityName = nameof(KontragentDebt);

            public static class Command
            {
                public static readonly string Topic = $"{Domain}.Command.{EntityName}";
            }
        }
    }
}