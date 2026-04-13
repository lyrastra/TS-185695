namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencySale
{
    public static class IncomingCurrencySaleConstants
    {
        public const string EntityName = "IncomingCurrencySale";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
