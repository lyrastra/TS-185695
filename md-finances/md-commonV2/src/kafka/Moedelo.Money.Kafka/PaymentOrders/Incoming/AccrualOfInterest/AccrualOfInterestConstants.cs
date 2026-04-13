namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.AccrualOfInterest
{
    public static class AccrualOfInterestConstants
    {
        public const string EntityName = "AccrualOfInterest";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
