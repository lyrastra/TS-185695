namespace Moedelo.Money.Kafka.PaymentOrders.Incoming.LoanReturn
{
    public static class LoanReturnConstants
    {
        public const string EntityName = "LoanReturn";

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
