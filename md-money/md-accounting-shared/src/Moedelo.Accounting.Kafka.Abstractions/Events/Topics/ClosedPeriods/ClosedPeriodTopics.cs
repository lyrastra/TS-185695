namespace Moedelo.Accounting.Kafka.Abstractions.Events.Topics.ClosedPeriods
{
    public class ClosedPeriodTopics
    {
        private static readonly string Domain = "ClosingWizards";

        public static class Event
        {
            public static readonly string EntityName = "ClosingPeriod";
            public static readonly string Topic = $"{Domain}.Event.{EntityName}";
        }
    }
}