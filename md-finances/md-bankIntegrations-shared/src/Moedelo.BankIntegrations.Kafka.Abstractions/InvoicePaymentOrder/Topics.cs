namespace Moedelo.BankIntegrations.Kafka.Abstractions.InvoicePaymentOrder
{
    public static class Topics
    {
        private const string DomainName = "BankIntegrations";
        
        public static class BankIntegrations
        {
            private const string SubDomainName = "InvoicePaymentOrder";

            public static class Changed
            {
                public const string EntityName = nameof(Changed);
                
                public static class Event
                {
                    public static readonly string Topic = $"{DomainName}.{SubDomainName}.Event.{EntityName}";
                }
            }
        }
    }
}