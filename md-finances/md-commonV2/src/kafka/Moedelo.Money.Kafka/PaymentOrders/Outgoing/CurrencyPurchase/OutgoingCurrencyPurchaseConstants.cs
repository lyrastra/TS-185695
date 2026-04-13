namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyPurchase
{
    public static class OutgoingCurrencyPurchaseConstants
    {
        public const string EntityName = "OutgoingCurrencyPurchase";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
