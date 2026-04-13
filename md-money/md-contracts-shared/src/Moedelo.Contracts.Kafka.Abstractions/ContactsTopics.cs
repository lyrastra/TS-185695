namespace Moedelo.Contracts.Kafka.Abstractions
{
    public class ContactsTopics
    {
        private const string DomainName = "Contracts";

        public static class Event
        {
            public static class Contract
            {
                public const string EntityName = nameof(Contract);
                public const string EventTopic = $"{DomainName}.Event.Contract";
            }
        }
    }
}