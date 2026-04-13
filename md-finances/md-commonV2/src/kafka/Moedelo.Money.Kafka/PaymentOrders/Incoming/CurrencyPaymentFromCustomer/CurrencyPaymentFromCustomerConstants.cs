namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPaymentFromCustomer
{
    public static class CurrencyPaymentFromCustomerConstants
    {
        public const string EntityName = "CurrencyPaymentFromCustomer";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
