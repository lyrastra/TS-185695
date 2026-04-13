namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromCash
{
    public static class TransferFromCashConstants
    {
        public const string EntityName = "TransferFromCash";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
