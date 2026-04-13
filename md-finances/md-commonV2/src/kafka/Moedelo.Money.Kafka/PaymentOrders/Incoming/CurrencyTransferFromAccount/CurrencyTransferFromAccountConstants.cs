namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.CurrencyTransferFromAccount
{
    public static class CurrencyTransferFromAccountConstants
    {
        public const string EntityName = "CurrencyTransferFromAccount";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
