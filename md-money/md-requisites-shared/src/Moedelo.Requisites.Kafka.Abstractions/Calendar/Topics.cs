namespace Moedelo.Requisites.Kafka.Abstractions.Calendar
{
    public static class Topics
    {
        private const string DomainName = "Calendar";

        //Entity
        public static class FirmCalendar 
        {
            public const string EntityName = nameof(FirmCalendar);

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }

        //Entity
        public static class Calendar
        {
            public const string EntityName = nameof(Calendar);

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}
