namespace Moedelo.BankIntegrations.Kafka.Abstractions.MovementRequest
{
    public static class Topics
    {
        private const string DomainName = "BankIntegrations";
        
        //Entity
        public static class MovementRequestEntity
        {
            public const string EntityName = "Movement";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}