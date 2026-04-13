namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToAccountablePerson
{
    public static class PaymentToAccountablePersonConstants
    {
        public const string EntityName = "PaymentToAccountablePerson";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
