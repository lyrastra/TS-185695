namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencySale
{
    public static class OutgoingCurrencySaleConstants
    {
        public const string EntityName = "OutgoingCurrencySale";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
