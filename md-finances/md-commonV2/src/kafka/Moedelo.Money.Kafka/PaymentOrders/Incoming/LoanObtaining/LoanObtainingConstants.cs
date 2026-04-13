namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanObtaining
{
    public static class LoanObtainingConstants
    {
        public const string EntityName = "LoanObtaining";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
