namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.PaymentToNaturalPersons
{
    public static class PaymentToNaturalPersonsConstants
    {
        public const string EntityName = "PaymentToNaturalPersons";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
