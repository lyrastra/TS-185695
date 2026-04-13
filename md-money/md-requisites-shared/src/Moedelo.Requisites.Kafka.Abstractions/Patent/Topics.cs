namespace Moedelo.Requisites.Kafka.Abstractions.Patent
{
    public static class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class PatentEntity
        {
            public const string EntityName = "Patent";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}