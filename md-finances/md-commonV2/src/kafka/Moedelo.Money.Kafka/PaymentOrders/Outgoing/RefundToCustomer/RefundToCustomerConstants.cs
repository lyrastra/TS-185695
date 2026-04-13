namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.RefundToCustomer
{
    public static class RefundToCustomerConstants
    {
        public const string EntityName = "RefundToCustomer";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
