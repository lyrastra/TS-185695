namespace Moedelo.Money.Kafka.PaymentOrders.Outgoing.LoanIssue
{
    public static class LoanIssueConstants
    {
        public const string EntityName = "LoanIssue";

        public static class Command
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Command.{EntityName}";
        }

        public static class Event
        {
            public static readonly string Topic = $"{MoneyConstants.Domain}.{PaymentOrdersConstants.Subdomain}.Event.{EntityName}.CUD";
        }
    }
}
