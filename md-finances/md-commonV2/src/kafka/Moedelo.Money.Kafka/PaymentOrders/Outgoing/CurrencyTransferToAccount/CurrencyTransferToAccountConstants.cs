namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.CurrencyTransferToAccount
{
    public static class CurrencyTransferToAccountConstants
    {
        public const string EntityName = "CurrencyTransferToAccount";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
