namespace Moedelo.BankIntegrations.Kafka.Abstractions.IntegratedUser
{
    public static class Topics
    {
        private const string DomainName = "Integrations";
        
        //Entity
        public static class IntegratedUserEntity
        {
            public const string EntityName = "IntegratedUser";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}