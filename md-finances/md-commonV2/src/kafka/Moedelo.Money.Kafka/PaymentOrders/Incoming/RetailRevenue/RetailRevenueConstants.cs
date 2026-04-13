namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.RetailRevenue
{
    public static class RetailRevenueConstants
    {
        public const string EntityName = "RetailRevenue";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
