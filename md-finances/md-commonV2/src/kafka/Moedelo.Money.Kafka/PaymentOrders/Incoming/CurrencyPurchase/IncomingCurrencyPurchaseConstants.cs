namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyPurchase
{
    public static class IncomingCurrencyPurchaseConstants
    {
        public const string EntityName = "IncomingCurrencyPurchase";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
