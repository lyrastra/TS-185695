namespace Moedelo.Billing.Kafka.Common;

public static class BillingTopics
{
    private const string DomainName = "Billing";

    public static class Receipts
    {
        private const string SubDomain = nameof(Receipts);

        public static class Receipt
        {
            public const string EntityName = nameof(Receipt);

            public static class Command
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Command.{EntityName}";
            }
        }
    }
        
    public static class Marketplace
    {
        private const string SubDomain = nameof(Marketplace);

        public static class ProlongationAttempt
        {
            public const string EntityName = nameof(ProlongationAttempt);

            public static class Event
            {
                public const string Topic = $"{DomainName}.{SubDomain}.Event.{EntityName}";
            }
        }
    }
}