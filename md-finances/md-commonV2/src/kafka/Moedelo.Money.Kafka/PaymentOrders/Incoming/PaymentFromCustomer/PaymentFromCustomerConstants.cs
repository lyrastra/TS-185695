namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.PaymentFromCustomer
{
    public static class PaymentFromCustomerConstants
    {
        public const string EntityName = "PaymentFromCustomer";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }

        public static class Event
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Event.{EntityName}.CUD";
        }
    }
}
