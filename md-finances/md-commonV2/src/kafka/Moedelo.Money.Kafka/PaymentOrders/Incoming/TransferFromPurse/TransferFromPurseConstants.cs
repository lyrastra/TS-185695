namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.TransferFromPurse
{
    public static class TransferFromPurseConstants
    {
        public const string EntityName = "TransferFromPurse";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
