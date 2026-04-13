namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToSupplier
{
    public static class PaymentToSupplierConstants
    {
        public const string EntityName = "PaymentToSupplier";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }

    }
}
