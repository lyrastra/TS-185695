namespace Moedelo.Money.Kafka.PaymentOrders.Import
{
    public static class PaymentOrderImportConstants
    {
        public const string EntityName = "PaymentOrder";

        public static class Event
        {
            public static readonly string Topic = $"PaymentOrderImport.Event.{EntityName}";
        }
    }
}
