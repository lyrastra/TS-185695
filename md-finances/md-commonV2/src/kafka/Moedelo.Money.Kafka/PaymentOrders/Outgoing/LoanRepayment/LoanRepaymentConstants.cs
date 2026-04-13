namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanRepayment
{
    public static class LoanRepaymentConstants
    {
        public const string EntityName = "LoanRepayment";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
