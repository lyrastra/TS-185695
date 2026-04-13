namespace Moedelo.Requisites.Kafka.Abstractions.AccountantForHour
{
    public static class Topics
    {
        private const string DomainName = "Requisites";

        //Entity
        public static class AccountantForHourEntity
        {
            public const string EntityName = "AccountantForHour";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}