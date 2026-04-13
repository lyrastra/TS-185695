namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromAccount
{
    public static class TransferFromAccountConstants
    {
        public const string EntityName = "TransferFromAccount";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
