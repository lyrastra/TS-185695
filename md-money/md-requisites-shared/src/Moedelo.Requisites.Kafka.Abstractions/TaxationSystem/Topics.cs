namespace Moedelo.Requisites.Kafka.Abstractions.TaxationSystem
{
    public static class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class TaxationSystemEntity
        {
            public const string EntityName = "TaxationSystem";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}