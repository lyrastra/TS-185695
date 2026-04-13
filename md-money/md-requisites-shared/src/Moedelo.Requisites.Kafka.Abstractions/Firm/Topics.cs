namespace Moedelo.Requisites.Kafka.Abstractions.Firm
{
    public static class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class FirmEntity
        {
            public const string EntityName = "Firm";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}