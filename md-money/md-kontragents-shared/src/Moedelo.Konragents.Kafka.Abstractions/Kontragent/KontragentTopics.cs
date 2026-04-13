namespace Moedelo.Konragents.Kafka.Abstractions
{
    public partial class KontragentsTopics
    {
        public static class Kontragent
        {
            public const string EntityName = nameof(Kontragent);

            public static class Event
            {
                public static readonly string Topic = $"{Domain}.Event.{EntityName}";
            }
        }
    }
}
