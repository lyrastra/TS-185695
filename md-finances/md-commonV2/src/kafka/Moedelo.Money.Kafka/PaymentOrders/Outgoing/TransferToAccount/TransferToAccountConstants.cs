namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.TransferToAccount
{
    public static class TransferToAccountConstants
    {
        public const string EntityName = "TransferToAccount";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
