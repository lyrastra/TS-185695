namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BankFee
{
    public static class OutgoingPaymentOrdersConstants
    {
        public const string EntityName = "Outgoing";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
