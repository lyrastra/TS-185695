namespace Moedelo.Requisites.Kafka.Abstractions.SettlementAccount
{
    public class Topics
    {
        private const string DomainName = "Requisites";
        
        //Entity
        public static class SettlementAccountEntity
        {
            public const string EntityName = "SettlementAccount";

            public static class Event
            {
                public static readonly string Topic = $"{DomainName}.Event.{EntityName}";
            }
        }
    }
}