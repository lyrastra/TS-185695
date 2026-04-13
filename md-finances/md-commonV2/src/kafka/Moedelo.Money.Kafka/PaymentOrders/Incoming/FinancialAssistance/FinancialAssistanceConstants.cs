namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.FinancialAssistance
{
    public static class FinancialAssistanceConstants
    {
        public const string EntityName = "FinancialAssistance";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }
    }
}
