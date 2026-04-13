namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.BudgetaryPayment
{
    public static class BudgetaryPaymentConstants
    {
        public const string EntityName = "BudgetaryPayment";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
