namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPaymentToSupplier
{
    public static class CurrencyPaymentToSupplierConstants
    {
        public const string EntityName = "CurrencyPaymentToSupplier";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
