using Moedelo.Money.Kafka.PaymentOrders;

namespace Moedelo.Money.Kafka.PurseOperations.Incoming.PaymentFromCustomer
{
    public static class PaymentFromCustomerConstants
    {
        public const string EntityName = "PaymentFromCustomer";

        public static class Event
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PurseOperationsConstants.Subdomain}.Event.{EntityName}";
        }
    }
}
